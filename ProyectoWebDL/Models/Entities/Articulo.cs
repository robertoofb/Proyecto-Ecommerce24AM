using System.ComponentModel.DataAnnotations;

namespace ProyectoWebDL.Models.Entities
{
    public class Articulo
    {
        [Key]
        public int PkArticulo { get; set; }

        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public double Precio { get; set; }
    }
}
