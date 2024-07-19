using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prova1_M_5.Models;
using Prova1_M_5.Service;

namespace Prova1_M_5.Controllers
{
    public class TipoViolazioneController : Controller
    {

        private readonly ILogger<TipoViolazioneController> _logger;
        private readonly ITipoViolazioneService _TipoViolazione;

        public TipoViolazioneController(ILogger<TipoViolazioneController> logger, ITipoViolazioneService tipoViolazioneService)
        {
            _logger = logger;
            _TipoViolazione = tipoViolazioneService;
        }


      
        public IActionResult Index()
        {
            var Viol = _TipoViolazione.GetAll();


            return View(Viol);
        }

       
        
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Create(TipoViolazione tipoViolazione)
        {
        _TipoViolazione.Add(tipoViolazione);
            return RedirectToAction(nameof(Index));
        
        }

      
    }
}
