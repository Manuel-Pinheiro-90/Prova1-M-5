using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prova1_M_5.Models;
using Prova1_M_5.Service;

namespace Prova1_M_5.Controllers
{
    public class AnagraficaController : Controller
    {

        private readonly ILogger<AnagraficaController> _logger;
        private readonly IAnagraficaService _anagraficaService;

        public AnagraficaController(ILogger<AnagraficaController> logger, IAnagraficaService anagraficaService)
        {
            _logger = logger;
            _anagraficaService = anagraficaService;
        }

        // GET: test
        public ActionResult Index()
        {
            var anagrafiche = _anagraficaService.GetAll();

            return View(anagrafiche);
           
        }


        public IActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]

        public IActionResult Create(Anagrafica anagrafica)
        {
            _anagraficaService.Add(anagrafica);
            return RedirectToAction(nameof(Index));
        }


      
     
      
    }
}
