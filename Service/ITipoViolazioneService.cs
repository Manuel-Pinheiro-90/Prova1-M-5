using Prova1_M_5.Models;

namespace Prova1_M_5.Service
{
    public interface ITipoViolazioneService
    {
        IEnumerable<TipoViolazione> GetAll();
        void Add(TipoViolazione tipoViolazione);
        
      
        TipoViolazione GetById(int id);



    }
}
