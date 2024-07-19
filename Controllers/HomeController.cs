using Microsoft.AspNetCore.Mvc;
using Prova1_M_5.Models;
using Prova1_M_5.Service;
using System.Diagnostics;

namespace Prova1_M_5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        

       

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
           
        }

        public  IActionResult Index()
        {


            return View();

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

      
        
       
      









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
