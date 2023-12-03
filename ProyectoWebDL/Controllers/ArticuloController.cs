using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoWebDL.Context;
using ProyectoWebDL.Models.Entities;
using ProyectoWebDL.Services.IServices;

namespace ProyectoWebDL.Controllers
{
    public class ArticuloController : Controller
    {
        //Constructor para el uso de base de datos
        private readonly IArticuloServices _articuloServices;
        private readonly ApplicationDbContext _context;

        public ArticuloController(IArticuloServices articuloServices, ApplicationDbContext context)
        {
            _articuloServices = articuloServices;
            _context = context;

        }

        [HttpGet]
        //Se retorna la vista "index" de la respectiva carpeta
        public async Task<IActionResult> Index()
        {
            try
            {
                //Uso de la lista de los articulos para que se muestre al abrir la vista

                return View(await _articuloServices.GetArticulos());

                /*var response = await _articuloServices.GetArticulos();
                return View(response);*/
            }
            catch (Exception ex)
            {
                throw new Exception("Succedio un error" + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> IndexCopia()
        {
            try
            {
                //Uso de la lista de los articulos para que se muestre al abrir la vista

                return View(await _articuloServices.GetArticulos());

                /*var response = await _articuloServices.GetArticulos();
                return View(response);*/
            }
            catch (Exception ex)
            {
                throw new Exception("Succedio un error" + ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Crear()
        {
            ViewBag.Categorias = _context.Categorias.Select(p => new SelectListItem()
            {
                Text = p.Nombre,
                Value = p.PkCategoria.ToString()
            });
            return View();
        }

        [HttpPost]
        public IActionResult Crear([FromForm] Articulo request)
        {
            try
            {
                var response = _articuloServices.CrearArticulo(request);
                //Esta funcion return sirve para volver al index despues de la accion
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                throw new Exception("Error" + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var response = await _articuloServices.GetByIdArticulo(id);

            ViewBag.Categorias = _context.Categorias.Select(p => new SelectListItem()
            {
                Text = p.Nombre,
                Value = p.PkCategoria.ToString()
            });
            return View(response);
        }

        [HttpPost]
        public IActionResult Editar([FromForm] Articulo request)
        {
            var response = _articuloServices.EditarArticulo(request);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            bool result = _articuloServices.EliminarArticulo(id);
            if (result = true)
            {
                return Json(new { succes = true });
            }
            else
            {
                return Json(new { succes = false });
            }
        }
    }
}
