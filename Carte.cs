using System;
namespace septica
{
    public class Carte
    {
        private string simbol;
        private string culoare;
        private string numar;
        public Carte(string simbol, string culoare, string numar)
        {
            this.simbol = simbol;
            this.culoare = culoare;
            this.numar = numar;
        }
        public string getSimbol()
        {
            return simbol;
        }
        public string getCuloare()
        {
            return culoare;
        }
        public string getNumar()
        {
            return numar;
        }
        public override string ToString()
        {
            return numar + " de " + simbol + " (" + culoare + ")";
        }
        public Carte()
        {
        }
    }
}

