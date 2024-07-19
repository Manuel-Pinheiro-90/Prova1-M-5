using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prova1_M_5.Service;

namespace Prova1_M_5.Controllers
{
    public class QueryController : Controller
    {
        private readonly ILogger<QueryController> _logger;
        private readonly IVerbaleService _verbaleService;

        public QueryController(ILogger<QueryController> logger, IVerbaleService verbaleService)
        {
            _logger = logger;
            _verbaleService = verbaleService;
        }





        public IActionResult TotaleVerbaliPerTrasgressore()
        {
            var result = _verbaleService.GetVerbaliPerTrasgressore();
            return View(result);
        }

        public IActionResult TotalePuntiDecurtatiPerTrasgressore()
        {
            var result = _verbaleService.GetPuntiDecurtatiPerTrasgressore();
            return View(result);
        }


        public IActionResult ViolazioniSuperano10Punti()
        {
            var result = _verbaleService.GetViolazioniSuperano10Punti();
            return View(result);
        }

        public IActionResult ViolazioniImportoMaggiore400()
        {
            var result = _verbaleService.GetViolazioniImportoMaggiore400();
            return View(result);
        }






    }
}
