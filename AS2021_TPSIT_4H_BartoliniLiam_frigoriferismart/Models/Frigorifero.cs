using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AS2021_TPSIT_4H_BartoliniLiam_frigoriferismart.Models
{
    class Frigorifero : Prodotto
    {
        public List<Prodotto> frigo = new List<Prodotto>();
        private readonly string path = @"C:\Users\Desktop\Desktop\AS2021_TPSIT_4H_BartoliniLiam_frigoriferismart\AS2021_TPSIT_4H_BartoliniLiam_frigoriferismart\prodottoFrigo.fr";

        public Frigorifero()
        {
            frigo.Add(new Prodotto(1, "Piada", "18/12/2020", 135, 2));        
            frigo.Add(new Prodotto(2, "Ragù", "19/12/2020", 350, 2));        
            frigo.Add(new Prodotto(3, "Acqua", "25/12/2030", 0, 3));
            frigo.Add(new Prodotto(4, "Torta", "23/12/2020", 530, 10));     
            frigo.Add(new Prodotto(5, "Minerali", "2/1/2021", 258, 1));     
        }

        public string ElencoProdottiScaduti()
        {
            StringBuilder sb = new StringBuilder();

            // Intestazione tabella
            sb.AppendLine("Elenco prodotti scaduti: ");
            sb.AppendLine("Prodotto\tData di scadenza\tPorzioni");

            for (int i = 0; i < frigo.Count; i++)
                if (DateTime.Compare(frigo[i].Scadenza, DateTime.Today) < 0)
                    sb.AppendLine($"{frigo[i].Descrizione}\t\t{frigo[i].Scadenza:dd/MM/yyyy}\t\t{frigo[i].Nprodotto}");

            return sb.ToString();
        }

        public string ElencoProdottiPresenti()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Codice\tDescrizione\tScadenza\tQuantità");
            for (int i = 0; i < frigo.Count; i++)
                if(frigo[i].Exist)
                    sb.AppendLine($"{frigo[i].CodiceIdentificativo}\t{frigo[i].Descrizione}\t\t{frigo[i].Scadenza:dd/MM/yyyy}\t{frigo[i].Nprodotto}");
            
            return sb.ToString();
        }

        public void PrelevamentoProdotto(int porzioni, int identificativo)
        {
            if (porzioni == frigo[identificativo - 1].Nprodotto)
                frigo[identificativo - 1].Exist = false;
            else if (porzioni <= frigo[identificativo - 1].Nprodotto)
                frigo[identificativo - 1].Nprodotto -= porzioni;
            else
                throw new Exception("Il numero di porzioni è maggiore di quello disponibile!");
        }

        public string PrelevamentoProdotto(int identificativo, DateTime scadenza)
        {
            StringBuilder sb = new StringBuilder();

            if (identificativo <= frigo.Count)
            {
                for (int i = 0; i < frigo.Count; i++)
                    if (DateTime.Compare(frigo[i].Scadenza, scadenza) == 0 && DateTime.Compare(frigo[identificativo - 1].Scadenza, scadenza) == 0) // Se la data e il codice identificativo corrispondono allo stesso prodotto stampo
                    {
                        if(frigo[identificativo - 1].Nprodotto > 1)
                            throw new Exception("Inserire il numero di porzioni che si prelevano: ");
                        
                        // Scrivo la tabella dell'output
                        sb.AppendLine("Il prodotto:\t" + frigo[identificativo - 1].Descrizione);
                        sb.AppendLine("Codice:\t\t" + frigo[identificativo - 1].CodiceIdentificativo.ToString());
                        sb.AppendLine($"Scadenza:\t{frigo[identificativo - 1].Scadenza:dd/MM/yyyy}");
                        sb.AppendLine("E' stato prelevato");

                        frigo[identificativo - 1].Exist = false;
                    }
            }
            else
                sb.AppendLine($"Codice {identificativo} non esiste!");

            return sb.ToString();
        }

        public string NumeroConfezioni(int identificativo)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Prodotto\tConfezioni");

            for (int i = 0; i < frigo.Count; i++)
                if(frigo[i].CodiceIdentificativo == identificativo)
                    sb.AppendLine($"{frigo[i].Descrizione}\t\t{frigo[i].Nprodotto}");
            
            return sb.ToString();
        }
        
        public string InserimentoProdotto(int identificativo, string descrizione, string scadenza, int kCal, int porzioni)
        {
            // Controllo se il codice identificativo è gia presente
            for (int i = 0; i < frigo.Count; i++)
                if (frigo[i].CodiceIdentificativo == identificativo - 1)
                    throw new Exception("Il codice identificativo è gia presente!");

            // Controllo se il prodotto è scaduto
            DateTime dt = Convert.ToDateTime(scadenza);
            if (DateTime.Compare(dt, DateTime.Today) < 0)
                throw new Exception("Il prodotto inserito è scaduto!");

            Prodotto p = new Prodotto(identificativo, descrizione, scadenza, kCal, porzioni);
            frigo.Add(p);

            return "Prodotto inserito correttamente";
        }

        public string SalvataggioDati()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"\nSalvataggio in giorno: {DateTime.Today:dd/MM/yyyy}");    
            sb.AppendLine("Identificativo\tDescrizione\t\tScadenza\tPorzioni\tKcals");

            for (int i = 0; i < frigo.Count; i++)
                if(frigo[i].Exist)
                    sb.AppendLine(frigo[i].ToString());
            
            sb.AppendLine("============");

            File.AppendAllText(path, sb.ToString());

            return "Salvataggio effettuato con successo!";
        }

        public string RipristinoDati()
        {
            File.WriteAllText(path, "");
            return "Ripristino avvenuto con successo";
        }
    }
}