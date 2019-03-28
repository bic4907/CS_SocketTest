using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Client
    {
        string ip;
        long last_timestmp;

        Client(string ip)
        {
            this.ip = ip;
        }



        void setIP(string ip) { this.ip = ip; }
        string getIP() { return this.ip; }

        void setLastTimestamp(long timestamp) { this.last_timestmp = timestamp; }
        long getLastTimeStamp() { return this.last_timestmp ; }

    }
}
