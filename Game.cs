using System;

namespace septica
{
    public enum ModJoc
    {
        SinglePlayer, // J1 uman vs J2 calculator
        MultiPlayer   // J1 uman vs J2 uman
    }
    //Game e creierul jocului si tine minte tot
    //pachet, maini, scor, penalizare si ce e pe masa
    //aplica regulile de joc si decide randul jucatorilor
    public class Game
    {
        // ===================== membrii =====================

        private Pachet pachet; //pachetul de 32 de carti de unde trag
        private Jucator j1; // uman
        private Jucator j2; // calculator sau uman (in multiplayer)
        private Carte cartePeMasa; // cartea curenta care este pe masa 
                                   // si totodata cartea de referinta
        private int scorJ1;
        private int scorJ2;
        private int penalizare; // cate carti trebuie sa traga un jucator din cauza ca s-a dat 7
        private bool randJ1; // true=randJ1, false=randJ2
        //tin minte o runda
        private Carte carteDeschidere; // prima carte din runda (cea cu care s-a deschis daca e null runda nu e deschisa, daca e !=null, runda e deschisa)
        private bool deschidereEsteJ1; //cine deschide runda (J1=true, J2=false)
        //mod joc
        private ModJoc mod; //singleplayer sau multiplayer


        // ===================== constructori =====================
        //constructor implicit, porneste jocul in modul singleplayer
        public Game()
        {
            mod = ModJoc.SinglePlayer;
            pachet = new Pachet();  // se construieste pachetul
            j1 = new JucatorUman(); // jucatorul 1 e om
            if (mod == ModJoc.SinglePlayer)
                j2 = new JucatorCalculator(); //daca mod e singleplayer, jucatorul 2 e calculatorul
            else
                j2 = new JucatorUman(); //daca mod e multiplayer, jucatorul 2 e om
            //initializare scoruri+penalizare
            scorJ1 = 0;
            scorJ2 = 0;
            penalizare = 0;
            randJ1 = true; //mereu incepe jucator1
            carteDeschidere = null; //nu exista runda deschisa inca
            deschidereEsteJ1 = true; //j1 va deschide runda

        }//porneste automat daca fac un new Game() fara param

        public Game(ModJoc mod)
        {
            this.mod = mod; 
            pachet = new Pachet();  
            j1 = new JucatorUman(); 
            if (mod == ModJoc.SinglePlayer)
                j2 = new JucatorCalculator(); 
            else
                j2 = new JucatorUman(); 
            scorJ1 = 0;
            scorJ2 = 0;
            penalizare = 0;
            randJ1 = true; 
            carteDeschidere = null; 
            deschidereEsteJ1 = true; 
        }

        // ===================== metode =====================
        //inceperea efectiva a jocului
        public void incepeJocul()
        {
            imparteCarti(); // amesteca pachetul si da cate 4 carti fiecarui jucator
            cartePeMasa = pachet.trageCarte(); //pune o carte initiala pe masa
            //reseteaza penalizarea, runda si randul
            penalizare = 0;
            randJ1 = true;
            carteDeschidere = null;
            deschidereEsteJ1 = true;
            // Pt modul singleplayer: daca cumva e randul calculatoruilui, il lasa sa joace
            if (mod == ModJoc.SinglePlayer && !randJ1)
                turaCalculator();
        }

        private void imparteCarti()
        {
            pachet.amesteca(); // apeleaza metoda amesteca din clasa pachet
            for (int i = 0; i < 4; i++)
                j1.primesteCarte(pachet.trageCarte());
            for (int i = 0; i < 4; i++)
                j2.primesteCarte(pachet.trageCarte());
        }




        // ---------------------- reguli ------------------------------
        //true daca ce carte s-a dat are acelasi nr sau simbol cu cartea de referinta
        public bool esteCarteValida(Carte c)
        {
            if (c == null) return false; // daca nu exista cartea pe care a selectst-o jucatorul, false
            if (cartePeMasa == null) return true; // daca masa e goala nu mai verific (improbabil sa fie masa goala)
            int nrCarteRef = conversieNumar(cartePeMasa.getNumar());
            int nrCarteJucata = conversieNumar(c.getNumar());
            string simbolCarteRef = cartePeMasa.getSimbol();
            string simbolCarteJucata = c.getSimbol();
            return (nrCarteRef == nrCarteJucata) || (simbolCarteRef == simbolCarteJucata);
        }

        //transforma nr cartilor din string in int si compara sa vada ce jucator da carte mai mare
        private int comparaCarti(Carte c1, Carte c2)
        {
            int n1 = conversieNumar(c1.getNumar());
            int n2 = conversieNumar(c2.getNumar());
            if (n1 > n2) return 1;
            if (n1 < n2) return 2;
            return 0;
        }
        //daca s-a jucat un 7 se face penalizare
        private void Penalizare(Carte c)
        {
            if (c != null && c.getNumar() == "7")
            {
                if (penalizare > 0) penalizare += 2; // daca nu exista penalizare se incepe cu doi
                else penalizare = 2; // daca exista se mai adauga 2
            }
        }

        // folosita la penalizare, da cate carti jucatorului penalizat, daca exista atatea in pachet
        private void trageCarti(Jucator j, int cate) //private caci doar Game decide cand se trag carti
        {
            for (int i = 0; i < cate; i++)
            {
                if (pachet.numarCarti() > 0)
                    j.primesteCarte(pachet.trageCarte()); // scoate o carte din pachet, o returneaza 
                                                           // si o pune in mana jucatorului
            }
        }




        // ---------------- gestionarea rundei -----------------------
        
        //deschide o runda
        private void deschideRunda(Carte c, bool esteJ1)
        {
            carteDeschidere = c; // reti prima carte din runda 
            deschidereEsteJ1 = esteJ1; // reti cine a jucat-o
            randJ1 = !esteJ1;// schimba randul ca sa raspunda celalalt
        }

        //cand cineva joaca o carte valida aici se decide ce se intampla
        private void proceseazaCarteJucata(Carte c, bool esteJ1)
        {
            if (c == null) return;
            cartePeMasa = c; //pune cartea pe masa
            Penalizare(c); // se face penalizare in caz ca s-a dat 7
            if (carteDeschidere == null) // daca carteDeschidere == null, incepe runda
            {
                deschideRunda(c, esteJ1);
                if (mod == ModJoc.SinglePlayer && !randJ1)
                    turaCalculator();
            }
            else // daca carteDeschidere != null, se incheie runda
            {
                inchideRunda(c);
            }
        }
        // s-a dat a doua carte din runda, deci e final de runda asa ca o inchidem
        private void inchideRunda(Carte carteRaspuns)
        {
            // decidem castigatorul undei runde si i dam punct
            int castigator = comparaCarti(carteDeschidere, carteRaspuns);
            // daca deschizatorul castiga, ia punct 
            // daca cel care raspunde castiga, ia punct
            if (castigator == 1)
            {
                if (deschidereEsteJ1) scorJ1++; //nu stim cine a dat cartea castigatoare 
                                                // de aceea verificam sa vedem a cui a fost randul
                else scorJ2++;
            }
            else if (castigator == 2)
            {
                if (deschidereEsteJ1) scorJ2++;
                else scorJ1++;
            }
            randJ1= deschidereEsteJ1; //seteaza randul urmator
            carteDeschidere = null;
            if (mod == ModJoc.SinglePlayer && !randJ1) // pentru singleplayer daca castiga runda calculatorul
                turaCalculator();
        }
        //se intampla cand un jucator nu are carte valida in mana si renunta(trage)
        private void castigaRundaPrinRenuntare()
        {
            //asa castiga deschizatorul rundei
            if (deschidereEsteJ1) scorJ1++;
            else scorJ2++;
            randJ1 = deschidereEsteJ1;
            carteDeschidere = null;
            if (mod == ModJoc.SinglePlayer && !randJ1)
                turaCalculator();
        }
        private bool existaRundaDeschisa()
        {
            return carteDeschidere != null; // runda deschisa = true
                                            // runda inchisa = false
        }
        private bool esteRaspuns(bool esteJ1) 
        {
            if (carteDeschidere == null) return false;
            return deschidereEsteJ1 != esteJ1; // daca deschizatorul e diferit de cine are randul
                                               // returneaza true daca nu returneaza false
        }

        // ===================== mutari J1 =====================
       //metode pe care le apelez din Form2
        public void joacaCarteJ1Selectata() // joaca pt J1 cartea pe care a selectat-o in interfata grafica
        {
            if (!(j1 is JucatorUman)) return; // verifica daca J1 e jucator uman (de siguranta)
            int index = ((JucatorUman)j1).getIndex(); // ia pozitia cartii selectate din jucatorul uman (prin index)
            joacaCarteJ1Selectata(index); // apeleaza varianta cu un parapentru a met jocacaCarteJ1Selectata
        }

        // joaca o carte pt J1, iar indexul vine ca parametru (asa pot primi in server mesajul PLAY|index)
        public void joacaCarteJ1Selectata(int index)
        {
            joacaCarteSelectata(true, index);
        }

        public void trageCarteJ1() // trage carte cand se apasa pe butonul btnTrageCarte
        {
            trageCarte(true);
        }


        // ===================== mutari J2 (pt multiplayer) =====================
        // doar in multiplayer (sunt apelate in clasa server)
        public void joacaCarteJ2Selectata(int index)
        {
            if (mod != ModJoc.MultiPlayer) return; // daca e singleplayer nu face nimic
            joacaCarteSelectata(false, index); 
        }

        public void trageCarteJ2()
        {
            if (mod != ModJoc.MultiPlayer) return; // targe carte daca e multiplayer (in singleplayer se trage cand se apeleaza turaCalculator())
            trageCarte(false);
        }

        // ===================== gestionarea mutarilor  =====================
        private void joacaCarteSelectata(bool esteJ1, int index)
        {
            // verificare rand (daca un jucator vrea sa joace da nu e randul lui opreste metoda)
            if (esteJ1 && !randJ1) return;
            if (!esteJ1 && randJ1) return;
            Jucator j = esteJ1 ? j1 : j2; // daca esteJ1=true, j=j1, j=j2;
            JucatorUman ju = j as JucatorUman;
            if (ju == null) return; //daca nu e jucator uman iesim
            // setez index+iau cartea aleasa din mana
            ju.setIndex(index);
            Carte aleasa = j.getCarteDinMana(index);
            if (aleasa == null) return; 
            // spenalizare cu 7
            if (penalizare > 0)
            { 
                Carte c7 = j.joacaSapteDacaAre(); // daca ai 7 il joaca automat
                if (c7 != null)
                {
                    proceseazaCarteJucata(c7, esteJ1); //joaca 7 automat si il proceseaza
                }
                else
                {
                    if (!j.areSapte()) // daca nu are 7, trage penalizarea, iar aceasta se reseteaza
                    {
                        trageCarti(j, penalizare);
                        penalizare = 0;

                        if (esteRaspuns(esteJ1)) // daca e raspuns, pierde runda
                            castigaRundaPrinRenuntare();
                        else
                        {
                            randJ1 = !esteJ1;
                            if (mod == ModJoc.SinglePlayer && !randJ1) // daca nu e rasp doar trece randul
                                turaCalculator();
                        }
                    }
                }
                return; // nu mai continua cu logica normala, fiinca penalizarea a fost de fapt runda
            }

            // validare carte
            if (!esteCarteValida(aleasa))
                return; // daca nu e valida cartea, nu ai voie sa joci

            Carte c1 = j.joacaCarte(cartePeMasa, pachet); //joaca efectiv cartea si proceseaza
            proceseazaCarteJucata(c1, esteJ1);
        }

        private void trageCarte(bool esteJ1)
        {
            // verificare rand
            if (esteJ1 && !randJ1) return;
            if (!esteJ1 && randJ1) return;
            Jucator j = esteJ1 ? j1 : j2;
            // penalizare
            if (penalizare > 0)
            {
                if (!j.areSapte())
                {
                    trageCarti(j, penalizare); // daca ai penalizare si nu ai 7, tragi penalizarea
                    penalizare = 0;

                    if (esteRaspuns(esteJ1))
                        castigaRundaPrinRenuntare();
                    else
                    {
                        randJ1 = !esteJ1;
                        if (mod == ModJoc.SinglePlayer && !randJ1)
                            turaCalculator();
                    }
                }
                return;
            }

            // daca e raspuns, pierzi runda prin renuntare
            if (esteRaspuns(esteJ1))
            {
                if (pachet.numarCarti() > 0)
                    j.primesteCarte(pachet.trageCarte());
                castigaRundaPrinRenuntare();
                return;
            }
            // altfel trage o carte si se schimba randul
            if (pachet.numarCarti() > 0)
                j.primesteCarte(pachet.trageCarte());
            randJ1 = !esteJ1;
            if (mod == ModJoc.SinglePlayer && !randJ1)
                turaCalculator();
        }

        private void turaCalculator()
        {
            // doar in single-player
            if (mod != ModJoc.SinglePlayer) return; //ruleaza doar daca e single-player
            if (randJ1) return; // si daca e randul lui J2
            // penalizare
            if (penalizare > 0)
            {
                if (j2.areSapte())
                {
                    // daca are 7, joaca 7
                    Carte c7 = j2.joacaSapteDacaAre();
                    if (c7 != null)
                    {
                        proceseazaCarteJucata(c7, false);
                        return;
                    }
                }
                // daca nu are 7, e penalizat 
                trageCarti(j2, penalizare);
                penalizare = 0;

                if (esteRaspuns(false)) // daca e raspuns in acest caz pierde runda prin renuntare
                {
                    castigaRundaPrinRenuntare();
                    return;
                }

                randJ1 = true;
                return;
            }
            // daca e raspuns si n-a primit penalizare, trebuie sa inchida runda
            if (esteRaspuns(false))
            {
                Carte c2 = j2.joacaCarte(cartePeMasa, pachet); //incearca sa joace carte valida
                if (c2 == null)
                {
                    castigaRundaPrinRenuntare();//daca nu gaseste carte valida, trage una
                                                // asta e considerata ca renuntare, iar adversarul castiga runda
                    return;
                }
                proceseazaCarteJucata(c2, false);
                return;
            }

            if (!existaRundaDeschisa()) // daca nu e runda, incearca sa deschida una 
            {
                Carte c2 = j2.joacaCarte(cartePeMasa, pachet);
                if (c2 != null)
                {
                    proceseazaCarteJucata(c2, false); 
                    return;
                }
                randJ1 = true;
                return;
            }

            randJ1 = true; // daca nu a facut nimic, paseaza randul lui J1
        }

        // verifica daca jucatorul are macar o carte valida de jucat pe masa curenta
        private bool poateJuca(Jucator j)
        {
            for (int i = 0; i < j.getNrCarti(); i++)
            {
                if (esteCarteValida(j.getCarteDinMana(i)))
                    return true;
            }
            return false;
        }

        public bool esteFinalDeJoc()
        {
            if (j1.getNrCarti() == 0) return true; // daca cineva a ramas fara carti e final
            if (j2.getNrCarti() == 0) return true;

            if (pachet.numarCarti() == 0) // daca nu mai sunt carti in pachet si nici carti valide 
                                          // in mana a cel putin unui jucator e final
            {
                if (!poateJuca(j1) || !poateJuca(j2))
                    return true;
            }

            return false;
        }

        // ===================== getters =====================
        public int getScorJ1() { return scorJ1; }
        public int getScorJ2() { return scorJ2; }

        public int getNrCartiJ1() { return j1.getNrCarti(); }
        public int getNrCartiJ2() { return j2.getNrCarti(); }

        public Carte getCartePeMasa() { return cartePeMasa; }

        public Carte getCarteDinMana1(int index)
        {
            if (index < 0 || index >= j1.getNrCarti())
                return null;
            return j1.getCarteDinMana(index);
        }

        public Carte getCarteDinMana2(int index)
        {
            if (index < 0 || index >= j2.getNrCarti())
                return null;
            return j2.getCarteDinMana(index);
        }

        public int getCastigatorFinal()
        {
            if (j1.getNrCarti() == 0) return 1;
            if (j2.getNrCarti() == 0) return 2;
            if (scorJ1 > scorJ2) return 1;
            if (scorJ2 > scorJ1) return 2;
            return 0;
        }

        public JucatorUman getJucatorUman()
        {
            return (JucatorUman)j1;
        }

        public Jucator getJucator2()
        {
            return j2;
        }

        public bool esteRandulJ1() { return randJ1; }
        public bool esteRandulJ2() { return !randJ1; }
        public bool esteMultiplayer() { return mod == ModJoc.MultiPlayer; }

        // ===================== conversie numar =====================
        private int conversieNumar(string numar)
        {
            switch (numar)
            {
                case "7": return 7;
                case "8": return 8;
                case "9": return 9;
                case "10": return 10;
                case "J": return 11;
                case "Q": return 12;
                case "K": return 13;
                case "A": return 14;
                default: return 0;
            }
        }
    }
}
