using Microsoft.AspNetCore.Mvc;
using ProyectoWebDL.Models;
using ProyectoWebDL.Services.IServices;
using System.Diagnostics;

namespace ProyectoWebDL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticuloServices _articuloServices;
        public HomeController(ILogger<HomeController> logger, IArticuloServices articuloServices)
        {
            _articuloServices= articuloServices;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task <IActionResult> Privacy()
        {
            var response = await _articuloServices.GetArticulos();
            return View(response);
        }
        public IActionResult Contacto()
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