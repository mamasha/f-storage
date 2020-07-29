using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace f_core
{
    public interface ILogger
    {
        void Info(string trackingId, object data, 
            [CallerFilePath] string file = null, [CallerMemberName] string func = null, [CallerLineNumber] int line = 0);

        void Error(string trackingId, Exception ex,
            [CallerFilePath] string file = null, [CallerMemberName] string func = null, [CallerLineNumber] int line = 0);

        void Stop();
    }

    public class Logger : ILogger
    {
        public static readonly ILogger Null = new NullLogger();

        class Msg
        {
            public string Severity { get; set; }
            public DateTime At { get; set; }
            public string TrackingId { get; set; }
            public string File { get; set; }
            public string Func { get; set; }
            public int Line { get; set; }
            public object Data { get; set; }
        }

        private readonly string _path;
        private readonly BlockingCollection<Msg> _que;
        private readonly Thread _thr;

        public static ILogger New(FConfig config)
        {
            return 
                new Logger(config);
        }

        private Logger(FConfig config)
        {
            _path = config.Server.LogFileName;
            _que = new BlockingCollection<Msg>();
            _thr = new Thread(mainLoop);

            _thr.Start();
        }

        private void log(Msg msg)
        {
            var file = Path.GetFileNameWithoutExtension(msg.File);

            var data = (msg.Data is string) ? msg.Data : msg.Data.ToJson();

            var str = $"" +
                $"{msg.At:MM/dd/yyyy HH:mm:ss} " +
                $"{msg.Severity} " +
                $"{msg.TrackingId} " +
                $"{file}.{msg.Func}.{msg.Line} " +
                $"{data}" +
                $"\r\n";

            File.AppendAllText(_path, str);
        }

        private void post(Msg msg)
        {
            _que.Add(msg);
        }

        private void mainLoop()
        {
            try
            {
                for (;;)
                {
                    var msg = _que.Take();

                    if (msg == null)
                        break;

                    try
                    {
                        log(msg);
                    }
                    catch (Exception)
                    { }
                }
            }
            catch (Exception)
            { }
        }

        void ILogger.Info(string trackingId, object data, string file, string func, int line)
        {
            var msg = new Msg { 
                Severity = "I",
                At = DateTime.Now,
                TrackingId = trackingId,
                File = file,
                Func = func,
                Line = line,
                Data = data
            };

            post(msg);
        }

        void ILogger.Error(string trackingId, Exception ex, string file, string func, int line)
        {
            var msg = new Msg { 
                Severity = "E",
                At = DateTime.Now,
                TrackingId = trackingId,
                File = file,
                Func = func,
                Line = line,
                Data = new { Error = ex.Message }
            };

            post(msg);
        }

        void ILogger.Stop()
        {
            post(null);
        }
    }

    public class NullLogger : ILogger
    {
        void ILogger.Info(string trackingId, object data, string file, string func, int line) { }
        void ILogger.Error(string trackingId, Exception ex, string file, string func, int line) { }
        void ILogger.Stop() { }
    }
}
