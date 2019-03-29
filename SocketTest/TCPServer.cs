using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketTest
{
    class TCPServer
    {

        private static TCPServer instance = null;
        private TcpListener server;
        private ServerController controller;
        private Thread aliveThr;
        private Thread serverThr;

        private List<Client> clients;

        internal TCPServer() {
            this.clients = new List<Client>();
            this.server = null;
            this.aliveThr = null;
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

        public static TCPServer GetInstance(ServerController controller)
        {
            if (instance == null)
            {
                instance = new TCPServer(controller);
            }
            return instance;
        }
        public static TCPServer GetInstance()
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
                if(server != null)
                {
                    controller.ShowMessage("서버가 이미 열려 있습니다.");
                    return false;
                }

                server = new TcpListener(IPAddress.Any, 8080);
                server.Start();

                serverThr = new Thread(new ThreadStart(ServerThread));
                serverThr.Start();
                StartAliveCheck();

                return true;
            } catch(SocketException e) {
                controller.ShowMessage("포트가 이미 사용 중 입니다.");
                Debug.Print(e.ToString());

                return false;
            } catch(Exception e)
            {
                Debug.Print(e.ToString());
                return false;
            }
        }

        internal List<Client> GetClients()
        {
            return this.clients;
        }

        private void ServerThread()
        {
            while (true)
            {
                if (server.Pending())
                {
                    TcpClient tc = server.AcceptTcpClient();
                    server.Start();
                    clients.Add(new Client(tc));
                    
                    this.controller.RefreshList();
                }


                try
                {
                    foreach (Client c in clients)
                    {
                        try
                        {
                            NetworkStream stream = c.GetSocket().GetStream();
                            if (stream.CanRead && stream.DataAvailable)
                            {


                                IFormatter formatter = new BinaryFormatter();
                                TCPClientMessage recvPacket = new TCPClientMessage();
                                recvPacket = (TCPClientMessage)formatter.Deserialize(stream);

                                if (recvPacket.Cmd == TCPClientCmd.PONG)
                                {
                                    Debug.Print(c.GetSocket().Client.RemoteEndPoint.ToString() + " pong!!!");
                                    this.pong(c, recvPacket);






                                }



                            }
                        } 
                        catch (SocketException) {
                            CloseClient(c);
                        }
                    }
                } catch(Exception e)
                {
                    Debug.Print(e.Message);
                }
            }
        }

        public void Stop()
        {
            if (serverThr != null)
            {
                serverThr.Abort();
                serverThr = null;
            }
            if (server != null)
            {
                server.Server.Close();
                server.Stop();
                server = null;
            }
        }

        public void CloseClient(Client c)
        {
            c.GetSocket().Close();
            this.clients.Remove(c);
        }
        public void CloseTcpClient(TcpClient tc)
        {
            tc.Close();
            this.clients.RemoveAll(x => x.GetSocket() == tc);
        }
        public void StartAliveCheck()
        {
            aliveThr = new Thread(new ThreadStart(delegate ()
            {
                while (true)
                {
                    try
                    {
                        foreach (Client c in clients)
                        {
                            ping(c.GetSocket());
                        }
                    } catch(Exception)
                    {

                    }
                    this.controller.RefreshList();
                    Thread.Sleep(500);
                }
                
            }));
            aliveThr.Start();
        }
        public void StopAliveCheck()
        {
            if (aliveThr == null && aliveThr.IsAlive)
            {
                aliveThr.Abort();
                aliveThr = null;
            }
        }

        public void ping(TcpClient tc)
        {
            try
            {
                if (tc.GetStream().CanWrite)
                {

                    NetworkStream stream = tc.GetStream();
                    IFormatter formatter = new BinaryFormatter();

                    TCPServerMessage pck = new TCPServerMessage();
                    pck.Cmd = TCPServerCmd.PING;
                    pck.Param = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
                    formatter.Serialize(stream, pck);

                    Debug.Print(tc.Client.RemoteEndPoint.ToString() + " ping!!");

                }
            }
            catch (Exception)
            {
                CloseTcpClient(tc);
            }
        }

        private void pong(Client tc, TCPClientMessage msg)
        {
            /* This is async method */
            tc.SetLastTimestamp(DateTimeOffset.Now.ToUnixTimeSeconds());
            tc.SetLatency(
                DateTimeOffset.Now.ToUnixTimeMilliseconds() - msg.RemoteTime
                );
        }


        


    }
}
