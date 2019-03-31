using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest
{

    class TCPTask
    {
        string uuid;
        Client client;
        TCPServerCmd cmd;
        string data;


        internal TCPTask() {
            uuid = Guid.NewGuid().ToString();
        }
        internal TCPTask(Client client, TCPServerCmd cmd, string data) : this()
        {
            this.client = client;
            this.cmd = cmd;
            this.data = data;
        }

        public Client GetClient()
        {
            return this.client;
        }
        public TCPServerCmd GetCommand()
        {
            return this.cmd;
        }
        public string GetData()
        {
            return this.data;
        }

    }
}
