﻿namespace Prova1_M_5.Models
{
    public class ViolazioniSuperano10PuntiViewModel
    {
        public decimal Importo { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public DateTime DataViolazione { get; set; }
        public int DecurtamentoPunti { get; set; }
    }
}
