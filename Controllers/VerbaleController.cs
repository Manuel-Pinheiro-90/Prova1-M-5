using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prova1_M_5.Models;
using Prova1_M_5.Service;

namespace Prova1_M_5.Controllers
{
    public class VerbaleController : Controller
    {
        private readonly ILogger<VerbaleController> _logger;
        
       private readonly IVerbaleService _verbaleService;
        private readonly IAnagraficaService _anagraficaService;
        private readonly ITipoViolazioneService _violazioneService; 
        public VerbaleController(ILogger<VerbaleController> logger, IVerbaleService verbaleService, IAnagraficaService anagraficaService, ITipoViolazioneService tipoviolazioneService )
        {
            _logger = logger;
            _verbaleService = verbaleService;
       _anagraficaService = anagraficaService;  
            _violazioneService = tipoviolazioneService;

        }

        public IActionResult Index()
        {
            var verb = _verbaleService.GetAll();

            return View(verb);

        }



        public IActionResult Create()
        {
            ViewBag.Anagrafiche = _anagraficaService.GetAll();
            ViewBag.TipiViolazione = _violazioneService.GetAll();
            return View(new Verbale());
        }

       
        [HttpPost]
       
        public IActionResult Create(Verbale verbale)
        {
           
           
             _verbaleService.Add(verbale);
               
            
          
             return RedirectToAction(nameof(Index));
        }



    }
}
