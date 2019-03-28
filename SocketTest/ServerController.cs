using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocketTest
{
    class ServerController
    {
        private TCPServer svInstance;
        private BasicForm view;

        internal ServerController() {
            svInstance = new TCPServer(this);
        }
        internal ServerController(BasicForm view) : this()
        {
            this.view = view;

            
            view.button1.Click += new EventHandler(delegate(object sender, EventArgs e) {
                if(this.StartServer())
                {
                    Debug.Print("서버켜짐");
                    this.ChangeStatusMessage("서버 켜짐");
                }

                

            });


        }

        internal void ChangeStatusMessage(string msg)
        {

            this.view.bottomServerStatus.Text = msg;
        }




        ~ServerController() { }


        bool StartServer()
        {
            return svInstance.Start();
        }




        internal void ShowMessage(string v,
            MessageBoxButtons btnType,
            MessageBoxIcon icon)
        {
            btnType = MessageBoxButtons.OK;
            icon = MessageBoxIcon.None;


            MessageBox.Show(v, "cation", btnType, icon);
        }


    }
}
