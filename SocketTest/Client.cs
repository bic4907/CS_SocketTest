using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest
{
    class Client
    {
        string ip;
        long last_timestmp;
        private TcpClient tc;
        private float latency;
        bool isActual;

        public bool IsActual { get => isActual; set => isActual = value; }

        public Client(TcpClient tc)
        {
            this.ip = tc.Client.RemoteEndPoint.ToString();
            this.last_timestmp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            this.tc = tc;
            this.latency = -1;
            this.isActual = false;
        }

        public void SetIP(string ip) { this.ip = ip; }
        public string GetIP() { return this.ip; }

        public void SetLastTimestamp(long timestamp) { this.last_timestmp = timestamp; }
        public long GetLastTimeStamp() { return this.last_timestmp ; }

        public void SetLatency(float ltc) { this.latency = ltc; }
        public float GetLatency() { return this.latency; }

        public TcpClient GetSocket() { return this.tc; }
    }
}
