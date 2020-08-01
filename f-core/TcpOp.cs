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
        private readonly int _bufSize;
        private readonly Stream _tcp;
        private readonly StreamReader _reader;
        private readonly StreamWriter _writer;

        public static ITcpOp New(int bufSize, Stream tcp)
        {
            return new TcpOp(bufSize, tcp);
        }

        private TcpOp(int bufSize, Stream tcp)
        {
            _bufSize = bufSize;
            _tcp = tcp;
            _reader = new StreamReader(tcp);
            _writer = new StreamWriter(tcp);
        }

        async Task<string> ITcpOp.ReadString()
        {
            var str = await _reader.ReadLineAsync();
            return str;
        }

        async Task<T> ITcpOp.Read<T>()
        {
            var json = await _reader.ReadLineAsync();
            return json.Parse<T>();
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
            var left = count;
            var buf = new byte[_bufSize];

            while (left > 0)
            {
                int chunk = (int) Math.Min(count, _bufSize);

                var bytesRead = await _tcp.ReadAsync(buf, 0, chunk);

                if (bytesRead == 0)
                    throw new ApplicationException($"No more bytes in stream count={count} left={left}");

                await dst.WriteAsync(buf, 0, bytesRead);

                left -= bytesRead;
            }
        }

        async Task ITcpOp.WriteBytesFrom(Stream src, long count)
        {
            await src.CopyToAsync(_tcp, _bufSize);
            await _tcp.FlushAsync();
        }
    }
}
