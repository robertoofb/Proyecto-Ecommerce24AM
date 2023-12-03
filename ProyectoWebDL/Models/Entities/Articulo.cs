using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Autor { get; set; }

        [ForeignKey("Categorias")]
        public int? FkCategoria { get; set; }
        public Categoria Categorias { get; set; }

        [NotMapped]
        [Display(Name = "Imagen")]
        public IFormFile Img { get; set; }
        public string UrlImagenPath { get; set; }

        [NotMapped]
        [Display(Name = "PDFArticulo")]
        public IFormFile Pdf { get; set; }
        public string UrlPdfPath { get; set; }
    }
}
