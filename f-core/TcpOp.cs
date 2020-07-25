using System.IO;
using System.Threading.Tasks;

namespace f_core
{
    public interface ITcpOp
    {
        Task<string> ReadString();
        Task WriteString(string str);
        Task ReadBytesTo(Stream dst, long count);
        Task WriteBytesFrom(Stream src, long count);
    }

    public class TcpOp : ITcpOp
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
            return str;
        }

        async Task ITcpOp.WriteString(string str)
        {
            await _writer.WriteLineAsync(str);
            await _writer.FlushAsync();
        }

        async Task ITcpOp.ReadBytesTo(Stream dst, long count)
        {
            await dst.CopyToAsync(_tcp);
        }

        async Task ITcpOp.WriteBytesFrom(Stream src, long count)
        {
            await src.CopyToAsync(_tcp);
            await _tcp.FlushAsync();
        }
    }
}
