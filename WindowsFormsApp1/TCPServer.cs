using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class TCPServer
    {

        private static TCPServer instance = null;
        private IPEndPoint server;
        private Socket serverSck;
        private ServerController controller;


        private List<Client> clients;

        internal TCPServer() {
            this.clients = new List<Client>();
            this.server = null;
            this.serverSck = null;
        }
        internal TCPServer(ServerController controller) : this()
        {
            this.controller = controller;
        }
        ~TCPServer() {
            this.clients.Clear();

            if (this.server != null)
            {
                this.Stop();
            }
        }

        TCPServer GetInstance()
        {
            if(instance == null)
            {
                instance = new TCPServer();
            }
            return instance;
        }

        public bool Start()
        {
            try {
                if(serverSck != null)
                {
                    controller.ShowMessage("서버가 이미 열려 있습니다.", MessageBoxButtons.OK, MessageBoxIcon.None);
                    return false;
                }

                serverSck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                server = new IPEndPoint(IPAddress.Any, 8080);
                serverSck.Bind(server);

                return true;
            } catch(SocketException e) {
                controller.ShowMessage("포트가 이미 사용 중 입니다.", MessageBoxButtons.OK, MessageBoxIcon.None);
                controller.ShowMessage(e.ToString(), MessageBoxButtons.OK, MessageBoxIcon.None);
                Debug.Print(e.ToString());

                return false;
            } catch(Exception e)
            {
                Debug.Print(e.ToString());
                return false;
            }




        }

        void Stop()
        {
            serverSck.Close();

            this.server = null;
        }
    }
}
