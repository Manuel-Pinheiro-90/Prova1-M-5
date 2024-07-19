namespace Prova1_M_5.Models
{
    public class Anagrafica
    {
        public int IdAnagrafica { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string Indirizzo { get; set; }
        public string Città { get; set; }

        public string CAP { get; set; }
        public string Cod_Fisc { get; set; }

        public ICollection<Verbale> Verbales { get; set; }  





    }
}
