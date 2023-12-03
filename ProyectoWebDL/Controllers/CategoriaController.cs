using Microsoft.AspNetCore.Mvc;
using ProyectoWebDL.Models.Entities;
using ProyectoWebDL.Services.IServices;

namespace ProyectoWebDL.Controllers
{
    public class CategoriaController : Controller
    {
        //Constructor para el uso de base de datos
        private readonly ICategoriaServices _categoriaServices;
        public CategoriaController(ICategoriaServices categoriaServices)
        {
            _categoriaServices = categoriaServices;
        }

        [HttpGet]
        //Se retorna la vista "index" de la respectiva carpeta
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _categoriaServices.GetCategoria());

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
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Categoria request)
        {
            try
            {
                var response = _categoriaServices.CrearCategoria(request);
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
            var response = await _categoriaServices.GetByIdCategoria(id);
            return View(response);
        }

        [HttpPost]
        public IActionResult Editar(Categoria request)
        {
            var response = _categoriaServices.EditarCategoria(request);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Eliminar(int id)
        {
            bool result = _categoriaServices.EliminarCategoria(id);
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
