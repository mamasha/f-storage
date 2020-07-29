using System;
using System.IO;
using System.Threading.Tasks;

namespace f_core
{
    interface ITcpOp
    {
        Task<string> ReadString();
        Task WriteString(string str);
        Task<T> Read<T>() where T : class;
        Task Write<T>(T data) where T : class;
        Task ReadBytesTo(Stream dst, long count);
        Task WriteBytesFrom(Stream src, long count);
    }

    class TcpOp : ITcpOp
    {
        private readonly Stream _tcp;
        private readonly StreamReader _reader;
        private readonly StreamWriter _writer;

        public static ITcpOp New(Stream tcp)
        {
            return new TcpOp(tcp);
        }

        private TcpOp(Stream tcp)
        {
            _tcp = tcp;
            _reader = new StreamReader(tcp);
            _writer = new StreamWriter(tcp);
        }

        async Task<string> ITcpOp.ReadString()
        {
            var str = await _reader.ReadLineAsync();

            if (str == null)
                throw new ApplicationException("Protocol error: null string");

            return str;
        }

        async Task<T> ITcpOp.Read<T>()
        {
            var json = await _reader.ReadLineAsync();

            if (json == null)
                throw new ApplicationException("Protocol error: null json");

            return
                json.Parse<T>();
        }

        async Task ITcpOp.Write<T>(T data)
        {
            var json = data.ToJson();

            await _writer.WriteLineAsync(json);
            await _writer.FlushAsync();
        }

        async Task ITcpOp.WriteString(string str)
        {
            await _writer.WriteLineAsync(str);
            await _writer.FlushAsync();
        }

        async Task ITcpOp.ReadBytesTo(Stream dst, long count)
        {
            const int bufSize = 81920;

            var buf = new byte[bufSize];

            while (count > 0)
            {
                int chunk = (int) Math.Min(count, bufSize);

                await _tcp.ReadAsync(buf, 0, chunk);
                await dst.WriteAsync(buf, 0, chunk);

                count -= chunk;
            }
        }

        async Task ITcpOp.WriteBytesFrom(Stream src, long count)
        {
            await src.CopyToAsync(_tcp);
            await _tcp.FlushAsync();
        }
    }
}
