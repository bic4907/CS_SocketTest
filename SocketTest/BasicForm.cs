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
        }

        private void BasicForm_Load(object sender, EventArgs e)
        {
        }

        internal void AddController(ServerController svController)
        {
            this.svController = svController;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
