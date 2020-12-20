using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AS2021_TPSIT_4H_BartoliniLiam_frigoriferismart.Models;

namespace AS2021_TPSIT_4H_BartoliniLiam_frigoriferismart
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creo l'oggetto e in automatico istanzio degli alimenti
            Frigorifero frigorifero = new Frigorifero();

            Console.WriteLine("Aprendo il frigorifero:\n" + frigorifero.ElencoProdottiPresenti());
            Console.WriteLine(frigorifero.ElencoProdottiScaduti());

            Console.WriteLine("\nInserire il codice identificativo e la data di scadenza di un prodotto per prelevarlo (codice,data): ");
            string strInput = Console.ReadLine();
            string[] inputs = strInput.Split(',');

            try
            {
                Console.WriteLine("Prelevamento prodotto...\n" + frigorifero.PrelevamentoProdotto(Convert.ToInt32(inputs[0]), Convert.ToDateTime(inputs[1])));
            }
            catch (Exception e)
            {
            prodotti:
                Console.WriteLine(e.Message);
                string strQuantita = Console.ReadLine();
                try
                {
                    frigorifero.PrelevamentoProdotto(Convert.ToInt32(strQuantita), Convert.ToInt32(inputs[0]));
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    goto prodotti;
                }
            }

            Console.WriteLine("Frigorifero aggiornato:\n" + frigorifero.ElencoProdottiPresenti());

            Console.WriteLine("Inserire un numero identificativo per sapere il numero di confezioni disponibili:");
            string strIdentificativo = Console.ReadLine();

            Console.WriteLine(frigorifero.NumeroConfezioni(Convert.ToInt32(strIdentificativo)));

            Console.WriteLine("Inserimento prodotto...");

        input:
            try
            {
                // Test case: 41,nuovo,22/12/2020,456,5
                Console.WriteLine("Inserisci un nuovo prodotto: ");
                string strNewProdotto = Console.ReadLine();
                string[] newProdotto = strNewProdotto.Split(',');

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(frigorifero.InserimentoProdotto(
                    Convert.ToInt32(newProdotto[0]), // Numero identificativo
                    newProdotto[1], // Descrizione
                    newProdotto[2], // Data di scadenza
                    Convert.ToInt32(newProdotto[3]), //calorie
                    Convert.ToInt32(newProdotto[4]) // Numero confezioni
                    ));

                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                goto input;
            }

            Console.WriteLine("\nFrigorifero aggiornato: \n" + frigorifero.ElencoProdottiPresenti());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(frigorifero.SalvataggioDati());
            Console.ResetColor();

            Console.WriteLine("Ripristinare il file (S/N):");

            switch (Console.ReadLine().ToUpper())
            {
                case "S":
                    Console.WriteLine("Ripristino in corso...");
                    goto accept;
                case "N":
                    Console.WriteLine("Ripristino rifiutato");
                    goto decline;
                default:
                    goto decline;
            }

        accept:
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(frigorifero.RipristinoDati());
            Console.ResetColor();
        decline:

            Console.ReadLine();
        }
    }
}