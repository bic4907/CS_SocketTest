using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SocketTest
{
    public partial class BasicForm : Form
    {
        ServerController svController;

        public BasicForm()
        {
            InitializeComponent();

            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BasicForm_Closing);
        }

        private void BasicForm_Load(object sender, EventArgs e)
        {
        }

        private void BasicForm_Closing(object sender, CancelEventArgs e)
        {
            this.svController.StopServer();
            Environment.Exit(0);
        }

        internal void AddController(ServerController svController)
        {
            this.svController = svController;
        }

        delegate void AddClientList_Invoke(Control ctrl, ListViewItem lvi);
        public void AddClientListView(Control ctrl, ListViewItem lvi)
        {
            if(ctrl.InvokeRequired)
            {
                AddClientList_Invoke invoke = new AddClientList_Invoke(AddClientListView);
                ctrl.Invoke(invoke, ctrl, lvi);
            } else
            {
                this.svClientList.Items.Add(lvi);
            }
        }
        delegate void RemoveClientList_Invoke(Control ctrl, int i);
        public void RemoveClientListView(Control ctrl, int i)
        {
            if (ctrl.InvokeRequired)
            {
                RemoveClientList_Invoke invoke = new RemoveClientList_Invoke(RemoveClientListView);
                ctrl.Invoke(invoke, ctrl, i);
            }
            else
            {
                this.svClientList.Items.RemoveAt(i);
            }
        }
        delegate ListViewItem GetClientListItem_Invoke(Control ctrl, int i);
        public ListViewItem GetClientListViewItem(Control ctrl, int i)
        {
            if (ctrl.InvokeRequired)
            {
                GetClientListItem_Invoke invoke = new GetClientListItem_Invoke(GetClientListViewItem);
                return (ListViewItem)ctrl.Invoke(invoke, ctrl, i);
            }
            else
            {
                return this.svClientList.Items[i];
            }
        }
        delegate void SetClientListItem_Invoke(Control ctrl, int i, ListViewItem lvi);
        public void SetClientListViewItem(Control ctrl, int i, ListViewItem lvi)
        {
            if (ctrl.InvokeRequired)
            {
                SetClientListItem_Invoke invoke = new SetClientListItem_Invoke(SetClientListViewItem);
                ctrl.Invoke(invoke, ctrl, i, lvi);
            }
            else
            {
                this.svClientList.Items[i].Text = lvi.SubItems[0].Text;
                this.svClientList.Items[i].SubItems[1].Text = lvi.SubItems[1].Text;
            }
        }

    }
}
