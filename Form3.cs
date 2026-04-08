using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace septica
{
   
    public partial class Form3 : Form
    {
        int playerId = 0;
        bool aDeschisForm4 = false;
        Client client;
        bool formInchis = true;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null) client.Deconecteaza();
        }

        private void Client_OnStatus(string msg)
        {
            if (!IsHandleCreated || IsDisposed) return;

            try
            {
                BeginInvoke(new MethodInvoker(delegate
                {
                    Log("CLIENT: " + msg);
                }));
            }
            catch { }
        }

        private void Client_OnMessage(string msg)
        {
            if (!IsHandleCreated || IsDisposed) return;

            try
            {
                BeginInvoke(new MethodInvoker(delegate
                {
                    Log("MSG: " + msg);

                    // daca serverul trimite START|1 sau START|2
                    if (!aDeschisForm4 && msg.StartsWith("START|"))
                    {
                        string[] parts = msg.Split('|');
                        if (parts.Length >= 2)
                        {
                            int id;
                            if (int.TryParse(parts[1], out id))
                            {
                                playerId = id;
                                aDeschisForm4 = true;

                                Form4 f4 = new Form4(client, playerId);
                                f4.Show();

                                // NU inchide Form3, doar ascunde (altfel se deconecteaza in FormClosing)
                                this.Hide();
                            }
                        }
                    }
                }));
            }
            catch { }
        }

        private void btn_Join_Click(object sender, EventArgs e)
        {
            client = new Client();
            client.OnStatus += Client_OnStatus;
            client.OnMessage += Client_OnMessage;
            client.Conecteaza(tb_IP.Text);
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (client != null) client.Deconecteaza(); 
            Form f = Application.OpenForms["Form1"];
            if (f != null)
                f.Show();
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (client != null) client.Deconecteaza();
            Application.Exit();
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

        private void lblStare_Click(object sender, EventArgs e)
        {

        }
    }
}

