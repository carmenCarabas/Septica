using System;

namespace septica
{
    public class JucatorCalculator : Jucator
    {
        public override Carte joacaSapteDacaAre()
        {
            for (int i = 0; i < nrCarti; i++)
            {
                if (mana[i] != null && mana[i].getNumar() == "7")
                {
                    Carte c = mana[i];
                    for (int j = i; j < nrCarti - 1; j++)
                        mana[j] = mana[j + 1];
                    mana[nrCarti - 1] = null;
                    nrCarti--;

                    return c;
                }
            }
            return null;
        }

        public override Carte joacaCarte(Carte cartePeMasa, Pachet pachet)
        {
            // cauta carte valida
            for (int i = 0; i < nrCarti; i++)
            {
                if (cartePeMasa == null || mana[i].getNumar() == cartePeMasa.getNumar() ||mana[i].getSimbol() == cartePeMasa.getSimbol())
                {
                    Carte c = mana[i];
                    for (int j = i; j < nrCarti - 1; j++)
                        mana[j] = mana[j + 1];
                    mana[nrCarti - 1] = null;
                    nrCarti--;
                    return c;
                }
            }

            // daca nu exista carte valida, trage o singura carte si paseaza
            if (pachet.numarCarti() > 0)
            {
                primesteCarte(pachet.trageCarte());
                return null;
            }

            // daca nu mai exista pachet, joaca prima carte
            if (nrCarti > 0)
            {
                Carte c = mana[0];
                for (int j = 0; j < nrCarti - 1; j++)
                    mana[j] = mana[j + 1];
                mana[nrCarti - 1] = null;
                nrCarti--;
                return c;
            }

            return null;
        }
    }
}
