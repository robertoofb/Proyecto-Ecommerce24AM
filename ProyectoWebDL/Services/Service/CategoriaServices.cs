using Microsoft.EntityFrameworkCore;
using ProyectoWebDL.Context;
using ProyectoWebDL.Models.Entities;
using ProyectoWebDL.Services.IServices;

namespace ProyectoWebDL.Services.Service
{
    public class CategoriaServices : ICategoriaServices
    {
        private readonly ApplicationDbContext _context;

        //Constructor para usar las tablas de base de datos
        public CategoriaServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> GetCategoria()
        {
            try
            {

                return await _context.Categorias.ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Surgio un error" + ex.Message);
            }

        }

        public async Task<Categoria> GetByIdCategoria(int id)
        {
            try
            {

                Categoria response = await _context.Categorias.FirstOrDefaultAsync(x => x.PkCategoria == id);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Surgio un error" + ex.Message);
            }

        }
        public async Task<Categoria> CrearCategoria(Categoria i)
        {
            try
            {
                Categoria request = new Categoria()
                {
                    PkCategoria = i.PkCategoria,
                    Nombre = i.Nombre,
                };

                var result = await _context.Categorias.AddAsync(request);
                _context.SaveChanges();

                return request;
            }
            catch (Exception ex)
            {
                throw new Exception("Surgio un error" + ex.Message);
            }
        }

        public async Task<Categoria> EditarCategoria(Categoria i)
        {
            try
            {

                Categoria categoria = _context.Categorias.Find(i.PkCategoria);

                categoria.PkCategoria = i.PkCategoria;
                categoria.Nombre = i.Nombre;

                _context.Entry(categoria).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return categoria;

            }
            catch (Exception ex)
            {
                throw new Exception("Succedio un error " + ex.Message);
            }
        }
        public bool EliminarCategoria(int id)
        {
            try
            {
                Categoria categorias = _context.Categorias.Find(id);

                if (categorias != null)
                {
                    var res = _context.Categorias.Remove(categorias);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Surgió un error: " + ex.Message);
            }
        }
    }
}
