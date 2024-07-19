using Prova1_M_5.Models;

namespace Prova1_M_5.Service
{
    public interface IVerbaleService
    {
        IEnumerable<VerbaleViewModel> GetAll();
        Verbale GetById(int id);
        void Add(Verbale verbale);

        IEnumerable<VerbaliPerTrasgressoreViewModel> GetVerbaliPerTrasgressore();
        IEnumerable <PuntiDecurtatiPerTrasgressoreViewModel> GetPuntiDecurtatiPerTrasgressore();
        IEnumerable<ViolazioniSuperano10PuntiViewModel> GetViolazioniSuperano10Punti();
        IEnumerable<ViolazioniImportoMaggiore400ViewModel> GetViolazioniImportoMaggiore400();


    }
}
