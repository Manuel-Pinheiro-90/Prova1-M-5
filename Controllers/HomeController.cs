using Microsoft.AspNetCore.Mvc;
using Prova1_M_5.Models;
using Prova1_M_5.Service;
using System.Diagnostics;

namespace Prova1_M_5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnagraficaService _anagraficaService;

       

        public HomeController(ILogger<HomeController> logger, IAnagraficaService anagraficaService)
        {
            _logger = logger;
            _anagraficaService = anagraficaService;
        }

        public  IActionResult Index()
        {
var anagrafiche =  _anagraficaService.GetAll();

            return View(anagrafiche);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        // /////////////////////////////////////////////////////

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
          









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
