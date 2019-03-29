using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest
{
    enum TCPServerCmd
    {
      PING = 0,
      PONG
    };

    [Serializable]
    class TCPServerMessage
    {
        private TCPServerCmd cmd;
        private string param;
        private long requestTime;
        private long responseTime;

        public string Param { get => param; set => param = value; }
        public long RequestTime { get => requestTime; set => requestTime = value; }
        public long ResponseTime { get => responseTime; set => responseTime = value; }
        internal TCPServerCmd Cmd { get => cmd; set => cmd = value; }
    }
}
