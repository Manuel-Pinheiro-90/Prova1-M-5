namespace Prova1_M_5.Models
{
    public class TipoViolazione
    {
        public int IdViolazione { get; set; }
        public string Descrizione { get; set; } 

        public ICollection<Verbale> Verbali {  get; set; }  


    }
}
