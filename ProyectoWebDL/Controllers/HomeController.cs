using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoWebDL.Context;
using ProyectoWebDL.Models;
using ProyectoWebDL.Models.Entities;
using ProyectoWebDL.Services.IServices;
using ProyectoWebDL.Services.Service;
using System.Diagnostics;

namespace ProyectoWebDL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticuloServices _articuloServices;
        private readonly IUsuarioServices _usuarioServices;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, IArticuloServices articuloServices, IUsuarioServices usuarioServices, ApplicationDbContext context)
        {
            _articuloServices= articuloServices;
            _usuarioServices = usuarioServices;
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _articuloServices.GetArticulos();
            return View(response);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public async Task<IActionResult> Investigadores()
        {
            var response = await _usuarioServices.GetInvestigadores();
            return View(response);
        }

        public async Task<IActionResult> Articulos()
        {
            var response = await _articuloServices.GetArticulos();
            return View(response);
        }

        public async Task<IActionResult> Miperfil()
        {
            var response = await _articuloServices.GetArticulos();
            return View(response);
        }

        public IActionResult ElegirArt()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SubirInv()
        {
            ViewBag.Categorias = _context.Categorias.Select(p => new SelectListItem()
            {
                Text = p.Nombre,
                Value = p.PkCategoria.ToString()
            });
            return View();
        }

        [HttpPost]
        public IActionResult SubirInv([FromForm] Articulo request)
        {
            try
            {
                var response = _articuloServices.PublicarArticulo(request);
                //Esta funcion return sirve para volver al index despues de la accion
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw new Exception("Error" + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            ViewBag.Roles = _context.Roles.Select(p => new SelectListItem()
            {
                Text = p.Nombre,
                Value = p.PkRoles.ToString()
            });
            return View();
        }



        [HttpPost]
        public IActionResult Registrar(Usuario request)
        {
            try
            {
                var response = _usuarioServices.RegistrarUsuario(request);
                //Esta funcion return sirve para volver al index despues de la accion
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw new Exception("Error" + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LoginUser(string user, string password)
        {

            var response = _context.Usuarios.Include(z => z.Roles)
                                                    .FirstOrDefault(x => x.NombreUsuario == user && x.Contraseña == password);


            if (response != null)
            {
                if (response.Roles.Nombre == "SA")
                {
                    return Json(new { Success = true, admin = true });
                }
                else if (response.Roles.Nombre == "Investigador")
                {
                    return Json(new { Success = true, investigador = true });
                }
                return Json(new { Success = true, investigador = false });
            }
            else
            {
                return Json(new { Success = false, admin = false });
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}