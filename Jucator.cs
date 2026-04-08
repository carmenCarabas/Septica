using System;

namespace septica
{
    public abstract class Jucator
    {
        protected Carte[] mana; //mana jucatorului
        protected int nrCarti; //cate carti are in mana

        public Jucator()
        {
            mana = new Carte[32];
            nrCarti = 0;
        }

        public void primesteCarte(Carte c) //primeste carte si o pune in mana
        {
            mana[nrCarti] = c;
            nrCarti++;
        }

        public Carte getCarteDinMana(int index) // da cartea de pe o pozitie, fara a o juca
        {
            if (index < 0 || index >= nrCarti)
                return null;
            return mana[index];
        }

        public int getNrCarti()
        {
            return nrCarti;
        }
        public bool areSapte() // cauta daca exista 7 in mana
        {
            for (int i = 0; i < nrCarti; i++)
                if (mana[i] != null && mana[i].getNumar() == "7")
                    return true;
            return false;
        }
        // metode abstracte
        public abstract Carte joacaSapteDacaAre();
        public abstract Carte joacaCarte(Carte cartePeMasa, Pachet pachet);
    }
}
