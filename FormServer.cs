using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Septica_Server
{
    public partial class FormServer : Form
    {
        Server server;
        bool formInchis = true;
        public FormServer()
        {
            InitializeComponent();
           
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server != null) server.Opreste();
        }

        private void Server_OnStatus(string msg)
        {
            if (!IsHandleCreated || IsDisposed)
                return;

            try
            {
                BeginInvoke(new MethodInvoker(delegate
                {
                    Log("SERVER: " + msg);
                }));
            }
            catch
            {
                // form-ul se inchide, ignoram
            }
        }

        private void Log(string msg)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        tbLog.AppendText(msg + Environment.NewLine);
                    }));
                }
                else
                {
                    tbLog.AppendText(msg + Environment.NewLine);
                }
            }
            catch
            {
               
            }
        }


        private void FormServer_Shown(object sender, EventArgs e)
        {
            server = new Server();
            server.OnStatus += Server_OnStatus;
            server.Porneste();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (server != null) server.Opreste();
            Application.Exit();
        }
    }
}

