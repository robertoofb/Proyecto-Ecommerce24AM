using ProyectoWebDL.Models.Entities;

namespace ProyectoWebDL.Services.IServices
{
    public interface ICategoriaServices
    {
        public Task<List<Categoria>> GetCategoria();
        public Task<Categoria> GetByIdCategoria(int id);

        public Task<Categoria> CrearCategoria(Categoria i);
        public Task<Categoria> EditarCategoria(Categoria i);
        public bool EliminarCategoria(int id);
    }
}
