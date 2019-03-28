using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class ServerController
    {
        private TCPServer svInstance;
        private Form1 view;

        internal ServerController() {
            svInstance = new TCPServer(this);
        }
        internal ServerController(Form1 view) : this()
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
            this.view.buttonStatusStrip.Text = msg;
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
