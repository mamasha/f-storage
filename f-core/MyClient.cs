using System;
using System.Threading.Tasks;

namespace f_core
{
    public class MyClient
    {
        private readonly FConfig _config;
        private readonly TimeSpan _needsPingPongSpan;

        private ClientInfo _info;
        private IClient _client;
        private DateTime _lastUsed;

        public MyClient(FConfig config)
        {
            _config = config;
            _needsPingPongSpan = TimeSpan.FromSeconds(config.Client.NeedsPingPongSeconds);
        }

        async public Task<IClient> GetInstance(ClientInfo info)
        {
            var now = DateTime.Now;

            var noPingPongIsRequired = now - _lastUsed < _needsPingPongSpan;

            _lastUsed = now;

            do
            {
                if (_client == null)
                    break;

                if (info.ServerName != _info.ServerName)
                    break;

                if (info.Port != _info.Port)
                    break;

                if (info.UserName != _info.UserName)
                    break;

                if (info.Password != _info.Password)
                    break;

                if (noPingPongIsRequired)
                    return _client;

                try
                {
                    await _client.Ping();
                    return _client;
                }
                catch   // just retry connection
                { }

            }
            while (false);

            if (_client != null)
                _client.Close();

            _info = info;
            _client = await FClient.New(_config, info);

            return _client;
        }
    }
}
