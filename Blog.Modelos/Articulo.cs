using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Modelos
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo titulo es requerido")]
        [Display(Name = "Titulo del Articulo")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El campo descripcion es requerido")]
        [Display(Name = "Descripcion del Articulo")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha de creacion")]
        public string FechaCreacion { get; set; }


        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImagen { get; set; }

        // mapeo
        [Required]
        public int CategoriaId { get; set; } 
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

    }
}
