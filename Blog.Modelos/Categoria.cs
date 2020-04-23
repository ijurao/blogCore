using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Modelos
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo nombre es requerido")]
        [Display(Name = "Nombre de la categoria")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo orden es requerido")]
        [Display(Name = "Orden de Visualizacion")]
        public int? Orden { get; set; }

    }
}
