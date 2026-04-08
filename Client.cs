using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace septica
{
    public class Client
    {
        TcpClient client; //obiectul care face conexiunea TCP catre server
        NetworkStream stream; //stream-ul prin care curg datele dupa conectarea la server
        StreamReader citire; //citeste text din stream linie cu linie
        StreamWriter scriere; // scrie text in stream linie cu linie 
        Thread t; // un thread separat care asculta serverul
        volatile bool asculta; // verifica daca mai asculta (citit in bucla din thread)
                               // volatile - ajuta ca schimbarea in false sa fie vazuta imediat de thread
        public event Action<string> OnStatus;   // pt Form3 (mesaje destare)
        public event Action<string> OnMessage;  // mesaje primite de la server

        public void Conecteaza(string ip) // se conecteaza la port 3000 si porneste thread-ul
        {
            ip = ip.Trim(); //taiem spatiile din ip
            try // incearca sa se conecteze la server la portul 3000
            {
                client = new TcpClient(ip, 3000);
                stream = client.GetStream(); //dupa conectare iei stream-ul de comunicare
                // creez un reader si writer pt text
                citire = new StreamReader(stream); 
                scriere = new StreamWriter(stream);
                scriere.AutoFlush = true; // cand fac WriteLine se trimite imediat (altfel undeori sta in buffer)
                asculta = true;
                t = new Thread(Asculta); //porneste thread-ul de ascultare
                t.IsBackground = true;
                t.Start();
                OnStatusSafe("Conectat la server!");
            }
            catch
            {
                OnStatusSafe("Nu mă pot conecta (IP/port/firewall).");
            }
        }

        private void Asculta() // citeste linii cu ReadLine() si le pune in OnMessageSafe
        {
            try
            {
                while (asculta) //cat timp asculta e true sta si asteapta o linie de la server
                {
                    string line = citire.ReadLine(); //ReadLine blocheaza pana vine mesaj
                    if (line == null) break; // null apare cand conexiunea s-a inchis
                    OnMessageSafe(line);
                }
            }
            catch
            {
                // conexiune inchisa sau eroare
            }

            asculta = false;
            OnStatusSafe("Deconectat.");
           
        }

        public void Trimite(string mesaj) // trimite mesaje cu WriteLine catre server
        {
            try
            {
                if (scriere != null)
                    scriere.WriteLine(mesaj);
            }
            catch { }
        }

        public void Deconecteaza() // opreste ascultarea + inchide stream/client
        {
            asculta = false; // opreste bucla din thread
            try { Trimite("#Gata"); } catch { } // trimite mesaj de oprire
            try { stream?.Close(); } catch { } // inchid stream
            try { client?.Close(); } catch { } // inchid socket
        }

        private void OnStatusSafe(string msg) // daca exista abonati(?.), ii notifica
        {
            try { OnStatus?.Invoke(msg); } catch { }
        }

        private void OnMessageSafe(string msg)
        {
            try { OnMessage?.Invoke(msg); } catch { } 
        }
    }
}
