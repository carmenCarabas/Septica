using System;

namespace septica
{
    public class Pachet
    {
        private Carte[] pachet = new Carte[32];
        private string[] numere = { "7", "8", "9", "10", "J", "Q", "K", "A" };
        private string[] simbol = { "cupa", "romb", "trefla", "pica" };
        public Pachet()
        {
            int index = 0;
            for (int i = 0; i < simbol.Length; i++)
            {
                for (int j = 0; j < numere.Length; j++)
                {
                    if (simbol[i] == "cupa" || simbol[i] == "romb")
                        pachet[index] = new Carte(simbol[i], "rosu", numere[j]);
                    else
                        pachet[index] = new Carte(simbol[i], "negru", numere[j]);
                    index++;
                }

            }
        }
        public void amesteca()
        {
            Random rnd = new Random(); 
            for (int i = 0; i < pachet.Length; i++)
            {
                int j = rnd.Next(0, pachet.Length);
                Carte aux;
                aux = pachet[i];
                pachet[i] = pachet[j];
                pachet[j] = aux;
            }
        }
        public Carte trageCarte()
        {
            Carte carteTrasa = pachet[0];
            for (int i = 0; i < pachet.Length - 1; i++)
            {
                pachet[i] = pachet[i + 1];
            }
            pachet[pachet.Length - 1] = null;
            return carteTrasa;
        }
        public int numarCarti()
        {
            int nrCarti = 0;
            for (int i = 0; i < pachet.Length; i++)
            {
                if (pachet[i] != null)
                    nrCarti++;
            }
            return nrCarti;
        }
    }

}
