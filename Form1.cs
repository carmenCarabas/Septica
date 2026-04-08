using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace septica
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_Instr_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                    "REGULI ȘEAPTICĂ\n" +
                    "---------------------------------\n" +
                    "\n" +
                    "1) Ai voie să joci o carte doar dacă se potrivește cu:\n" +
                    "   • numărul de pe masă  SAU\n" +
                    "   • simbolul de pe masă.\n" +
                    "\n" +
                    "2) Dacă nu poți / nu vrei să joci, poți apăsa „Trage carte”.\n" +
                    "\n" +
                    "3) Penalizare cu 7:\n" +
                    "   • Când cineva joacă 7, următorul jucător are penalizare +2 cărți.\n" +
                    "   • Dacă următorul jucător are 7, îl dă și penalizarea se mărește cu încă +2.\n" +
                    "   • Dacă nu are 7, trebuie să tragă toate cărțile de penalizare.\n" +
                    "\n" +
                    "4) Runda:\n" +
                    "   • Primul jucător „deschide” runda cu o carte.\n" +
                    "   • Celălalt jucător răspunde cu o carte validă.\n" +
                    "   • Cartea mai mare câștigă runda și dă 1 punct.\n" +
                    "\n" +
                    "5) Final:\n" +
                    "   • Dacă un jucător rămâne fără cărți -> câștigă.\n" +
                    "   • Altfel, la final se compară scorurile.\n" +
                    "\n" +
                    "ONLINE:\n" +
                    "   • Clientul doar trimite comenzi (PLAY / DRAW),\n" +
                    "     serverul decide dacă mutarea este validă.\n",
                    "Instrucțiuni joc",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );

        }

        private void btn_Despre_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Proiect realizat de: Cărăbaș Carmen-Maria\n" +
                              "An: II\n" +
                              "Specializarea: Calculatoare\n" +
                              "An universitar 2025-2026", "Despre", MessageBoxButtons.OK,
                               MessageBoxIcon.Information);
        }
    }
}
