using System;
using System.Drawing;
using System.Windows.Forms;

namespace septica
{
    public partial class Form2 : Form
    {
        private Game joc;
        public Form2()
        {
            InitializeComponent();
            joc = new Game(ModJoc.SinglePlayer);
            joc.incepeJocul();
            afiseazaCartiJ1();
            afiseazaMasa();
            afiseazaScor();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        // ================= da o carte J1 =================
        private void btnCarte_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int index = (int)btn.Tag;
            joc.getJucatorUman().setIndex(index);
            joc.joacaCarteJ1Selectata();
            afiseazaCartiJ1();
            afiseazaMasa();
            afiseazaScor();
            verificaFinal();
        }
        // ================= afisare carti J1 =================
        private void afiseazaCartiJ1()
        {
            flowLayoutPanelJ1.Controls.Clear();
            int nr = joc.getNrCartiJ1();
            for (int i = 0; i < nr; i++)
            {
                Button btn = new Button();
                btn.Width = 100;
                btn.Height = 140;
                btn.Tag = i;
                btn.Click += btnCarte_Click;
                Carte c = joc.getCarteDinMana1(i);
                afiseazaCartePeButon(btn, c);
                flowLayoutPanelJ1.Controls.Add(btn);
            }
        }

        // ================= afisare masa =================
        public void afiseazaMasa()
        {
            afiseazaCartePeButon(btnMasaJ1, joc.getCartePeMasa());
        }

        // ================= afisare scor =================
        private void afiseazaScor()
        {
            lblScor.Text = "Scor J1: " + joc.getScorJ1() + " | Scor J2: " + joc.getScorJ2();
        }

        // ================= afisare carte pe buton =================
        private void afiseazaCartePeButon(Button btn, Carte c)
        {
            if (c == null)
            {
                btn.BackgroundImage = null;
                btn.Text = "";
                return;
            }
            string numar = c.getNumar();
            string simbol = c.getSimbol();
            string numeFisier = numar + simbol + ".png";
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

        // ================= buton trage carte =================
        private void btnTrageCarte_Click(object sender, EventArgs e)
        {
            joc.trageCarteJ1();
            afiseazaCartiJ1();
            afiseazaMasa();
            afiseazaScor();
            verificaFinal();
        }

        private void verificaFinal()
        {
            if (joc.esteFinalDeJoc())
            {
                int castigator = joc.getCastigatorFinal();
                if (castigator == 1)
                    MessageBox.Show("Ai câștigat!");
                else if (castigator == 2)
                    MessageBox.Show("Calculatorul a câștigat!");
                else
                    MessageBox.Show("Egal!");

            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form f = Application.OpenForms["Form1"];
            if (f != null)
                f.Show();
        }

        private void jocNouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            joc = new Game(ModJoc.SinglePlayer);
            joc.incepeJocul();
            afiseazaCartiJ1();
            afiseazaMasa();
            afiseazaScor();
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMasaJ1_Click(object sender, EventArgs e)
        {

        }
    }
}
