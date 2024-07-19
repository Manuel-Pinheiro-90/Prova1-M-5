using Prova1_M_5.Models;

namespace Prova1_M_5.Service
{
    public interface IAnagraficaService
    {
        IEnumerable<Anagrafica> GetAll();

        Anagrafica GetByID(int id);
        
        void Add(Anagrafica anagrafica);
     
    
    
    
    }
}
