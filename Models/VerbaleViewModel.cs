namespace Prova1_M_5.Models
{
    public class VerbaleViewModel
    {
        public int IdVerbale { get; set; }
        public DateTime DataViolazione { get; set; }
        public string IndirizzoViolazione { get; set; }
        public string Nominativo_Agente { get; set; }
        public DateTime DataTrascrizioneVerbale { get; set; }
        public decimal Importo { get; set; }
        public int DecurtamentoPunti { get; set; }

        public int IdAnagrafica { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Indirizzo { get; set; }
        public string Città { get; set; }
        public string CAP { get; set; }
        public string Cod_Fisc { get; set; }

        public int IdViolazione { get; set; }
        public string Descrizione { get; set; }
    }
}
