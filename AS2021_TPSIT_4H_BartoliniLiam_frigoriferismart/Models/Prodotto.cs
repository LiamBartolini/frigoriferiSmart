using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS2021_TPSIT_4H_BartoliniLiam_frigoriferismart.Models
{
    class Prodotto
    {
        int _codiceIdentificativo;
        public int CodiceIdentificativo { get => _codiceIdentificativo; }

        string _descrizione;
        public string Descrizione { get => _descrizione; }

        DateTime _scadenza;
        public DateTime Scadenza { get => Convert.ToDateTime($"{_scadenza:dd/MM/yyyy}"); }
        
        int _kCals;
        public int Kcals { get => _kCals; }

        string _tipologiaPiatto;

        int _nPorzioni;
        public int Nprodotto { get => _nPorzioni; set => _nPorzioni = value; }

        public Prodotto()
        {
            Random rn = new Random();
            _codiceIdentificativo = rn.Next(0, 101);

            _descrizione = "Non disponibile";
            _scadenza = DateTime.Today;
        }
        
        public Prodotto(int id, string descrizione, string scadenza, int calorie, string tipologiaPiatto, int nPorzioni)
        {
            _codiceIdentificativo = id;
            _descrizione = descrizione;
            _scadenza = Convert.ToDateTime(scadenza);
            _kCals = calorie;
            _tipologiaPiatto = tipologiaPiatto;
            _nPorzioni = nPorzioni;
        }

        public override string ToString() => $"\t{_codiceIdentificativo}\t\t\t{_descrizione}\t\t\t{_scadenza:dd/MM/yyyy}\t\t{_nPorzioni}\t\t{_kCals}";
    }
}