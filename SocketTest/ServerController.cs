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
        /* Server */
        private TCPServer svInstance;

        /* Client */
        private TCPClient clInstance;
        

        private BasicForm view;

        internal ServerController() {
            svInstance = TCPServer.GetInstance(this);
            clInstance = TCPClient.GetInstance(this);
        }
        internal ServerController(BasicForm view) : this()
        {
            this.view = view;

            /* Server Part */
            view.button1.Click += new EventHandler(delegate(object sender, EventArgs e) {
                if(this.StartServer())
                {
                    Debug.Print("서버 온라인");
                    this.ChangeStatusMessage("서버 온라인");
                }
            });

            /* Client Part */
            view.connectBtn.Click += new EventHandler(delegate (object sender, EventArgs e)
            {
                if (this.ConnecetServer())
                {
                    Debug.Print("서버 접속 요청");
                }

            });
        }

        private bool ConnecetServer()
        {
            return clInstance.Connect();



        }

        internal void ChangeStatusMessage(string msg)
        {

            this.view.bottomServerStatus.Text = msg;
        }




        ~ServerController() { }


        public bool StartServer()
        {
            return svInstance.Start();
        }
        public void StopServer()
        {
            svInstance.Stop();
        }



        internal void ShowMessage(
            string v,
            string title = "알림",
            MessageBoxButtons btnType = MessageBoxButtons.OK, 
            MessageBoxIcon icon = MessageBoxIcon.None
            )
        {

            MessageBox.Show(v, title, btnType, icon);
        }

        [System.ComponentModel.Browsable(false)]
        internal void RefreshList()
        {
            Dictionary<string, Client> ipHash = new Dictionary<string, Client>();
            List<int> deleteIdx = new List<int>();


            foreach(Client c in svInstance.GetClients()) {
                ipHash.Add(c.GetIP(), c);
            }

            for (int i = 0; i < this.view.svClientList.Items.Count; i++)
            {

                ListViewItem lvi = this.view.GetClientListViewItem(this.view.ActiveControl, i);
                string ipInfo = lvi.Text;
                if (ipHash.ContainsKey(key: lvi.Text)) {

                    ListViewItem tmp_lvi = new ListViewItem();
                    tmp_lvi.Text = ipInfo;
                    tmp_lvi.SubItems.Add(ipHash[ipInfo].GetLastTimeStamp().ToString());

                    this.view.SetClientListViewItem(this.view.ActiveControl, i, tmp_lvi);

                    ipHash.Remove(ipInfo);

                } else
                {
                    this.view.RemoveClientListView(this.view.ActiveControl, i);
                    i--;
                }

                
                
            }


            foreach (KeyValuePair<string, Client> pair in ipHash)
            {
                
                ListViewItem lvi = new ListViewItem();
                lvi.Text = pair.Key;
                lvi.SubItems.Add(pair.Value.GetLastTimeStamp().ToString());
                this.view.AddClientListView(this.view.ActiveControl, lvi);
            }
            


        }
    }



}
