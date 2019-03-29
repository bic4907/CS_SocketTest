using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest
{
    enum TCPClientCmd
    {
        PING = 0,
        PONG,
    };

    [Serializable]
    class TCPClientMessage
    {
        private TCPClientCmd cmd;
        private string param;
        private long requestTime;
        private long responseTime;

        public TCPClientMessage() { }
        public TCPClientMessage(TCPServerMessage msg)
        {
            this.requestTime = msg.RequestTime;
        }

        public string Param { get => param; set => param = value; }
        public long RemoteTime { get => requestTime; set => requestTime = value; }
        public long ResponseTime { get => responseTime; set => responseTime = value; }
        internal TCPClientCmd Cmd { get => cmd; set => cmd = value; }
    }
}
