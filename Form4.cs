using System;
using System.Drawing;
using System.Windows.Forms;

namespace septica
{
    public partial class Form4 : Form
    {
        private Client client;
        private int playerId;    // 1 sau 2 (cine sunt eu)
        private int turnId;      // 1 sau 2 (cine are rand)
        private Carte[] mana = new Carte[32];
        private int nrCarti = 0;
        private Carte carteMasa = null;
        private int scorJ1 = 0, scorJ2 = 0;
        private int finalFlag = 0;     // 0 nu, 1 da
        private int winnerId = -1;     // -1 = inca nu e final, 0 egal, 1/2 castigator
        private bool aAratatFinal = false;

        public Form4(Client c, int pid)
        {
            InitializeComponent();
            client = c; // salveaza conexiunea la server
            playerId = pid; //seteaza playerID primit
            lblJucator.Text = (playerId == 1) ? "Jucător1" : "Jucător2"; 
            lblRand.Text = "";
            client.OnMessage += Client_OnMessage; // cand clientul primeste linie de la server se apeleaza Client_OnMessage
            // ca sa nu ratezi primul STATE
            client.Trimite("GETSTATE"); // trimite getstate imediat
        }

        private void Client_OnMessage(string msg) // primeste mesaje din retea
        {
            if (!IsHandleCreated || IsDisposed) return;

            try
            {
                BeginInvoke(new MethodInvoker(delegate
                {
                    if (msg.StartsWith("STATE|"))
                    {
                        CitesteState(msg);
                        AfiseazaTot();
                        VerificaFinal();
                    }

                    if (msg.StartsWith("INFO|"))
                    {
                        string text = msg.Substring(5);
                        MessageBox.Show(text, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }));
            }
            catch { }
        }



        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try { client.OnMessage -= Client_OnMessage; } catch { }
            base.OnFormClosing(e);
        }

        // ================== STATE ==================
        private void CitesteState(string msg)
        {
            try
            {
                string[] parts = msg.Split('|'); // imparte mesajul in bucati
                for (int i = 0; i < parts.Length; i++)
                {
                    string p = parts[i];
                    if (p.StartsWith("YOU:"))
                    {   // pt fiecare YOU actualizeaza player id 
                        int pid;
                        if (int.TryParse(p.Substring(4), out pid))
                        {
                            playerId = pid;
                            lblJucator.Text = (playerId == 1) ? "Jucător1" : "Jucător2";
                        }
                    }
                    if (p.StartsWith("TURN:"))
                    { // actualizeaza TurnID
                        int tid;
                        if (int.TryParse(p.Substring(5), out tid))
                            turnId = tid;
                    }
                    if (p.StartsWith("MASA:"))
                    {
                        carteMasa = ParseCarte(p.Substring(5));
                    }
                    if (p.StartsWith("SCOR:"))
                    { // actualizeaza scorul 
                        string[] s = p.Substring(5).Split('-');
                        if (s.Length == 2)
                        {
                            int.TryParse(s[0], out scorJ1);
                            int.TryParse(s[1], out scorJ2);
                        }
                    }
                    if (p.StartsWith("HAND:"))
                    {
                        // goleste mana locala si reseteaza numaratoarea
                        for (int k = 0; k < mana.Length; k++) mana[k] = null;
                        nrCarti = 0;
                        // ia mana de carti din mesaj
                        // o separa prin virgula
                        //
                        string lista = p.Substring(5);
                        if (lista != "")
                        {
                            string[] carti = lista.Split(',');
                            for (int k = 0; k < carti.Length; k++)
                            {
                                Carte c = ParseCarte(carti[k]); // pt fiecare cuvant face ParseCarte
                                if (c != null)
                                {
                                    mana[nrCarti] = c; // pune cartea in mana
                                    nrCarti++; // creste nr carti
                                }
                            }
                        }
                    }
                    if (p.StartsWith("FINAL:")) // seteaza finalFlag
                    {
                        int f;
                        if (int.TryParse(p.Substring(6), out f))
                            finalFlag = f;
                    }

                    if (p.StartsWith("WIN:")) // seteaza winnerID
                    {
                        int w;
                        if (int.TryParse(p.Substring(4), out w))
                            winnerId = w;
                    }
                }
            }
            catch
            {
                // ignore
            }
        }

        private Carte ParseCarte(string token)
        {
            token = (token ?? "").Trim();
            if (token == "" || token == "-") return null; // inseamna ca nu exista carte
            string[] a = token.Split('-'); // separa (ex "10-inima" in "10", "inima")
            if (a.Length != 2) return null;
            string numar = a[0];
            string simbol = a[1];
            string culoare = (simbol == "cupa" || simbol == "romb") ? "rosu" : "negru"; // decide culoarea in functie de simbol

            return new Carte(simbol, culoare, numar); // creeaza obiectul carte pt form
        }

        // ================== AFISARE (ca in Form2) ==================
        private void AfiseazaTot()
        {
            AfiseazaCarti();
            AfiseazaMasa();
            AfiseazaScor();
            AfiseazaRand();
        }

        private void AfiseazaCarti()
        {
            flowLayoutPanelJ1.Controls.Clear(); // goleste flowLayoutPanel-ul

            for (int i = 0; i < nrCarti; i++)
            {
                Button btn = new Button(); // creeaza cate un buton pt fiecare carte
                btn.Width = 100;
                btn.Height = 140;
                btn.Tag = i;
                btn.Click += btnCarte_Click;
                PuneImagine(btn, mana[i]); // pune imaginea cartii pe buton
                flowLayoutPanelJ1.Controls.Add(btn);
            }
        }

        private void AfiseazaMasa()
        {
            PuneImagine(btnMasaJ1, carteMasa);
        }

        private void AfiseazaScor()
        {
            lblScor.Text = "Scor J1: " + scorJ1 + " | Scor J2: " + scorJ2;
        }

        private void AfiseazaRand()
        {
           if (turnId == 1) lblRand.Text = "Rând: Jucător1";
           else if (turnId == 2) lblRand.Text = "Rând: Jucător2";
           else lblRand.Text = "Rând: ?";
        }

        private void PuneImagine(Button btn, Carte c)
        {
            if (c == null)
            {
                btn.BackgroundImage = null;
                btn.Text = "";
                return;
            }

            string numeFisier = c.getNumar() + c.getSimbol() + ".png";
            string cale = Application.StartupPath + "\\poze\\" + numeFisier;

            if (System.IO.File.Exists(cale))
            {
                btn.BackgroundImage = Image.FromFile(cale);
                btn.BackgroundImageLayout = ImageLayout.Stretch;
                btn.Text = "";
            }
            else
            {
                btn.BackgroundImage = null;
                btn.Text = c.ToString();
            }
        }

        // ================== ACTIUNI ==================
        private void btnCarte_Click(object sender, EventArgs e)
        {
            if (turnId != playerId)
            {
                MessageBox.Show("Nu e rândul tău!"); // daca nu e randul tau nu trimiti nimic
                return;
            }

            Button btn = (Button)sender;
            int index = (int)btn.Tag;
            client.Trimite("PLAY|" + index); // trimit comanda 
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnTrageCarte_Click_1(object sender, EventArgs e)
        {
            if (turnId != playerId)
            {
                MessageBox.Show("Nu e rândul tău!");
                return;
            }

            client.Trimite("DRAW");
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // inchidem jocul online, ne deconectam
            try { client.Trimite("QUIT"); } catch { }
            try { client.Deconecteaza(); } catch { }

            // aratam meniul principal
            Form f = Application.OpenForms["Form1"];
            if (f != null) f.Show();

            this.Close();
        }

        private void jocNouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // la online: cerem serverului joc nou (serverul decide)\
            aAratatFinal = false;
            try { client.Trimite("NEWGAME"); } catch { }
        }


        // ================== FINAL + MESSAGEBOX ==================
        private void VerificaFinal()
        {
            if (aAratatFinal) return;
            if (finalFlag != 1) return;

            aAratatFinal = true;

            if (winnerId == 1)
                MessageBox.Show("Jucător1 a câștigat!");
            else if (winnerId == 2)
                MessageBox.Show("Jucător2 a câștigat!");
            else
                MessageBox.Show("Egal!");
        }
    }
}
