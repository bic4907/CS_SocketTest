using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest
{
    enum TCPTaskCmd
    {
        
    }

    class TCPTask
    {
        string uuid;
        Client client;
        TCPTaskCmd cmd;
        string data;


        internal TCPTask() {
            uuid = Guid.NewGuid().ToString();
        }
        TCPTask(Client client, TCPTaskCmd cmd, string data) : this()
        {
            this.client = client;
            this.cmd = cmd;
            this.data = data;
        }

        public Client GetClient()
        {
            return this.client;
        }


    }
}
