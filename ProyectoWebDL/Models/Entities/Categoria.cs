using System.ComponentModel.DataAnnotations;

namespace ProyectoWebDL.Models.Entities
{
    public class Categoria
    {
        [Key]
        public int PkCategoria { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}
