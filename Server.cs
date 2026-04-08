using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using septica;

namespace Septica_Server
{
    public class Server
    {
        private TcpListener server; // asculta pe port
        private Thread tAccept; // thread separat pt acceptarea clientilor
        private volatile bool ruleaza; // indicator server pornit
        public event Action<string> OnStatus; // trimit Log catre FormServer
        // conexiuni separate + citire/ scriere separate pt fiecare client
        private TcpClient c1 = null; 
        private TcpClient c2 = null;
        private StreamReader r1 = null;
        private StreamReader r2 = null;
        private StreamWriter w1 = null;
        private StreamWriter w2 = null;
        // thread pt ascultarea fiecarui client
        private Thread tClient1 = null;
        private Thread tClient2 = null;
        // jocul real ruleaza doar pe server
        private Game joc = null;
        private object locker = new object(); // folosit la log ca s aprocesez un singur mesaj o data 

        //porneste serverul pe portul 3000 pt orice IP
        public void Porneste()
        {
            ruleaza = true;
            server = new TcpListener(IPAddress.Any, 3000);
            server.Start();
            // porneste thread-ul care face accept la clienti
            tAccept = new Thread(AsteaptaClienti);
            tAccept.IsBackground = true;
            tAccept.Start();
            OnStatusSafe("Server pornit. Aștept 2 jucători...");
        }

        private void AsteaptaClienti() 
        {
            while (ruleaza) // mereu cauta sa aiba doi clienti conectati
            {
                try
                {
                    OnStatusSafe("Aștept 2 jucători...");
                    // accepta primul client (blocheaza pana vine) si ii face reader/writer
                    c1 = server.AcceptTcpClient();
                    w1 = new StreamWriter(c1.GetStream());
                    w1.AutoFlush = true;
                    r1 = new StreamReader(c1.GetStream());
                    OnStatusSafe("Client 1 conectat!");
                    //la fel pt al doilea
                    c2 = server.AcceptTcpClient();
                    w2 = new StreamWriter(c2.GetStream());
                    w2.AutoFlush = true;
                    r2 = new StreamReader(c2.GetStream());
                    OnStatusSafe("Client 2 conectat!");

                    // porneste jocul real (multiplayer)
                    joc = new Game(ModJoc.MultiPlayer);
                    joc.incepeJocul();

                    // trimitem START + spunem fiecarui client ce ID are
                    try { w1.WriteLine("START|1"); } catch { }
                    try { w2.WriteLine("START|2"); } catch { }
                    OnStatusSafe("START trimis la ambii clienti.");

                    // trimitem prima stare
                    TrimiteStateLaAmbii();

                    // pornim thread-urile de ascultare pt comenzile celor doi clienti
                    tClient1 = new Thread(AscultaClient1);
                    tClient1.IsBackground = true;
                    tClient1.Start();
                    tClient2 = new Thread(AscultaClient2);
                    tClient2.IsBackground = true;
                    tClient2.Start();

                    // serverul "sta" pana cand unul se deconecteaza 
                    while (ruleaza && c1 != null && c2 != null)
                        Thread.Sleep(500);

                    // cand iese de aici => a picat cineva => reluam bucla si asteptam iar 2 clienti
                }
                catch
                {
                    ResetServer();
                }
            }
        }
        // inchide tot sa poata accepta iar 2 clienti fara sa repornesc aplicatia server
        private void ResetServer()
        {
            // inchidem tot si eliberam sloturile
            try { r1?.Close(); } catch { }
            try { r2?.Close(); } catch { }
            try { w1?.Close(); } catch { }
            try { w2?.Close(); } catch { }
            try { c1?.Close(); } catch { }
            try { c2?.Close(); } catch { }
            r1 = null; r2 = null;
            w1 = null; w2 = null;
            c1 = null; c2 = null;
            tClient1 = null;
            tClient2 = null;
            joc = null;
            OnStatusSafe("Server resetat. Aștept reconectare...");
        }


        private void AscultaClient1()
        {
            AscultaClient(1, r1);
        }

        private void AscultaClient2()
        {
            AscultaClient(2, r2);
        }


        private void AscultaClient(int playerId, StreamReader r)
        {
            // citeste mesaje de la client
            // daca conexiunea se inchide  ReadLine da null
            try
            {
                while (ruleaza)
                {
                    string line = r.ReadLine();
                    if (line == null) break;
                    ProceseazaMesaj(playerId, line);
                }
            }
            catch { }

            OnStatusSafe("Client " + playerId + " deconectat.");

            //anuntam adversarul INAINTE de reset
            AnuntaAdversarul(playerId);

            //reset ca sa permitem reconectarea
            ResetServer();
        }
        private void AnuntaAdversarul(int playerId)
        {
            try
            {
                if (playerId == 1)
                {
                    // a plecat J1 -> anuntam J2
                    if (w2 != null) w2.WriteLine("INFO|Adversarul s-a deconectat. Aștept reconectare...");
                }
                else
                {
                    // a plecat J2 -> anuntam J1
                    if (w1 != null) w1.WriteLine("INFO|Adversarul s-a deconectat. Aștept reconectare...");
                }
            }
            catch { }
        }



        private void ProceseazaMesaj(int playerId, string msg)
        {
            lock (locker) // daca ambii clienti trimit simultan se proceseaza unul, iar apoi celalalt
            {
                // log + daca jocul nu e pornit ignora
                OnStatusSafe("De la client " + playerId + ": " + msg);
                if (joc == null) return;

                // GETSTATE
                if (msg == "GETSTATE")
                {
                    TrimiteStateLaAmbii();
                    return;
                }
                // PLAY|index
                if (msg.StartsWith("PLAY|"))
                {
                    string[] parts = msg.Split('|'); // parseaza index
                    if (parts.Length >= 2)
                    {
                        int index;
                        if (int.TryParse(parts[1], out index)) //out param de iesire (poate returna mai multe val)
                        {
                            // aplica mutarea in game
                            if (playerId == 1) joc.joacaCarteJ1Selectata(index);
                            else joc.joacaCarteJ2Selectata(index);
                            //trimite noua stare ambilor
                            TrimiteStateLaAmbii();
                        }
                    }
                    return;
                }
                // DRAW
                if (msg == "DRAW")
                {
                    if (playerId == 1) joc.trageCarteJ1();
                    else joc.trageCarteJ2();

                    TrimiteStateLaAmbii();
                    return;
                }
                // QUIT / #Gata
                if (msg == "QUIT" || msg == "#Gata")
                {
                    OnStatusSafe("Client " + playerId + " a iesit.");
                    return;
                }
                if (msg == "NEWGAME")
                {
                    joc = new septica.Game(septica.ModJoc.MultiPlayer);
                    joc.incepeJocul();
                    TrimiteStateLaAmbii();
                    OnStatusSafe("Joc nou pornit (NEWGAME).");
                    return;
                }
            }
        }

        private void TrimiteStateLaAmbii() // fievcare primeste mana lui nu mana adversarului
        {
            try
            {
                if (w1 != null) w1.WriteLine(BuildStateForPlayer(1));
                if (w2 != null) w2.WriteLine(BuildStateForPlayer(2));
            }
            catch { }
        }


        private string BuildStateForPlayer(int playerId)
        {
            int turn = joc.esteRandulJ1() ? 1 : 2; // cine ia randul (true daca e j1)
            string masa = CarteToNet(joc.getCartePeMasa()); // returneaza obiectul cartePeMasa transformat in text
            string scor = joc.getScorJ1() + "-" + joc.getScorJ2();
            // mana doar pentru jucatorul respectiv
            string hand = "";
            // mana jucatorului
            if (playerId == 1)
            {
                for (int i = 0; i < joc.getNrCartiJ1(); i++)
                {
                    if (i > 0) hand += ",";
                    hand += CarteToNet(joc.getCarteDinMana1(i)); // tranforma carte din obiect intr-un text simplu
                }
            }
            else
            {
                for (int i = 0; i < joc.getNrCartiJ2(); i++)
                {
                    if (i > 0) hand += ",";
                    hand += CarteToNet(joc.getCarteDinMana2(i));
                }
            }

            // final + castigator
            int finalFlag = joc.esteFinalDeJoc() ? 1 : 0;
            int winnerId = -1; // -1 = inca nu e final
            if (finalFlag == 1)
                winnerId = joc.getCastigatorFinal();

            return "STATE|YOU:" + playerId +
                   "|TURN:" + turn +
                   "|MASA:" + masa +
                   "|SCOR:" + scor +
                   "|HAND:" + hand +
                   "|FINAL:" + finalFlag +
                   "|WIN:" + winnerId;
        }
        


        private string CarteToNet(septica.Carte c) // tranforma carte din obiect intr-un text simplu
        {
            if (c == null) return "-";
            return c.getNumar() + "-" + c.getSimbol();
        }

        private void OnStatusSafe(string msg) // trimite un mesaj de stare catre interfata serverului
        {
            try
            {
                if (ruleaza && OnStatus != null)
                    OnStatus(msg);
            }
            catch { }
        }

        // opreste complet serverul si inchide toate conexiunile
        public void Opreste()
        {
            ruleaza = false;
            try { server.Stop(); } catch { }
            try { c1?.Close(); } catch { }
            try { c2?.Close(); } catch { }
        }
    }
}
