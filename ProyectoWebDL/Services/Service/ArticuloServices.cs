using Microsoft.EntityFrameworkCore;
using ProyectoWebDL.Context;
using ProyectoWebDL.Models.Entities;
using ProyectoWebDL.Services.IServices;

namespace ProyectoWebDL.Services.Service
{
    public class ArticuloServices : IArticuloServices
    {
        private readonly ApplicationDbContext _context;

        //Constructor para usar las tablas de base de datos
        public ArticuloServices(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<List<Articulo>> GetArticulos()
        {
            try
            {

                return await _context.Articulos.ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Surgio un error" + ex.Message);
            }

        }

        public async Task<Articulo> GetByIdArticulo(int id)
        {
            try
            {
                //Articulo response = await _context.Articulos.FindAsync(id);

                Articulo response = await _context.Articulos.FirstOrDefaultAsync(x => x.PkArticulo == id);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Surgio un error" + ex.Message);
            }

        }
        public async Task<Articulo> CrearArticulo(Articulo i)
        {
            try
            {
                Articulo request = new Articulo()
                {
                    Nombre = i.Nombre,
                    Descripcion = i.Descripcion,
                    Precio = i.Precio,
                };

                var result = await _context.Articulos.AddAsync(request);
                 _context.SaveChanges();

                return request;
            }
            catch (Exception ex)
            {
                throw new Exception("Surgio un error" + ex.Message);
            }
        }

        public async Task<Articulo> EditarArticulo(Articulo i)
        {
            try
            {

                Articulo articulo = _context.Articulos.Find(i.PkArticulo);

                articulo.Nombre = i.Nombre;
                articulo.Descripcion = i.Descripcion;
                articulo.Precio = i.Precio;

                _context.Entry(articulo).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return articulo;

            }
            catch (Exception ex)
            {
                throw new Exception("Succedio un error " + ex.Message);
            }
        }
        public bool EliminarArticulo(int id)
        {
            try
            {
                Articulo articulo = _context.Articulos.Find(id);

                if(articulo != null)
                {
                    var res = _context.Articulos.Remove(articulo);
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
