using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Blog.Modelos
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo titulo es requerido")]
        [Display(Name = "Titulo del Articulo")]
        public string Titulo { get; set; }

        

        [Display(Name = "Fecha de creacion")]
        public string FechaCreacion { get; set; }


        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImagen { get; set; }

        
    }
}
