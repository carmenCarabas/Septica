using System;

namespace septica
{
    public class JucatorUman : Jucator
    {
        private int indexSelectat;
        public void setIndex(int index)
        {
            indexSelectat = index;
        }
        public int getIndex()
        {
            return indexSelectat;
        }
        public override Carte joacaCarte(Carte cartePeMasa, Pachet pachet)
        {
            Carte c = mana[indexSelectat];

            for (int i = indexSelectat; i < nrCarti - 1; i++)
                mana[i] = mana[i + 1];

            mana[nrCarti - 1] = null;
            nrCarti--;

            return c;
        }
        public override Carte joacaSapteDacaAre()
        {
            if (indexSelectat < 0 || indexSelectat >= nrCarti) return null;
            if (mana[indexSelectat] == null) return null;
            if (mana[indexSelectat].getNumar() != "7") return null;
            Carte c = mana[indexSelectat]; //joaca 7
            for (int i = indexSelectat; i < nrCarti - 1; i++)
                mana[i] = mana[i + 1];
            mana[nrCarti - 1] = null;
            nrCarti--;
            return c;
        }
    }
}
