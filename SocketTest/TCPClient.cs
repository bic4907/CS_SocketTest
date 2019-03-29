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
    class TCPClient
    {
        static TCPClient instance;
        ServerController controller;
        TcpClient mainTc;
        Thread mainSckThr;

        internal TCPClient()
        {
            mainTc = null;
            mainSckThr = null;
        }
        internal TCPClient(ServerController controller) : this()
        {
            this.controller = controller;
        }
        ~TCPClient()
        {

        }
        public static TCPClient GetInstance(ServerController controller)
        {
            if (instance == null)
            {
                instance = new TCPClient(controller);
            }
            return instance;
        }
        public static TCPClient GetInstance()
        {
            if (instance == null)
            {
                instance = new TCPClient();
            }
            return instance;
        }



        internal bool Connect()
        {
            try
            {
                if (mainTc != null)
                {
                    controller.ShowMessage("이미 접속중 입니다.");
                    return false;
                }
                mainTc = new TcpClient();
                mainTc.Connect(IPAddress.Loopback.ToString(), 8080);
                
                Debug.Print("서버 연결됨");
                mainSckThr = new Thread(new ThreadStart(MainThread));
                mainSckThr.Start();


                return true;
            }
            catch (SocketException e)
            {
                controller.ShowMessage("서버가 오프라인 상태입니다.");
                mainTc = null;
                Debug.Print(e.ToString());
                return false;
            }
            catch (Exception e)
            {
                mainTc = null;
                Debug.Print(e.ToString());
                return false;
            }
        }

        private void MainThread() {
            while(true)
            {
                

                NetworkStream stream = mainTc.GetStream();

                if (stream.CanRead && stream.DataAvailable)
                {
                    IFormatter formatter = new BinaryFormatter();
                    TCPServerMessage recvPacket = new TCPServerMessage();
                    recvPacket = (TCPServerMessage)formatter.Deserialize(stream);

                    if(recvPacket.Cmd == TCPServerCmd.PING)
                    {
  
                        TCPClientMessage sendPacket = new TCPClientMessage(recvPacket);
                        sendPacket.Cmd = TCPClientCmd.PONG;
                        formatter.Serialize(stream, sendPacket);

                        




                    }

                }


            }

        }



    }
}
