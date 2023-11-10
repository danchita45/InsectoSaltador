using EjemploAnnete.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EjemploAnnete.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Objeto1> list = new List<Objeto1>();
            for (int i = 0; i < 10; i++)
            {
                Objeto1 obj1 = new Objeto1();
                obj1.Altitude = i;
                obj1.Description = "Daniel";
                list.Add(obj1); 
            }

            ViewData["Objetos"] = list;
            return View(list);
        }

        public IActionResult Privacy()
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