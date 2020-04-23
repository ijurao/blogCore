using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Modelos
{
    public class ApplicationUser: IdentityUser
    {
        [Required(ErrorMessage="Ingrese un nombre ")]
     
        public string Nombre { get; set; }

        public string Direccion { get; set; }

        [Required(ErrorMessage = "Ingrese una ciudad ")]
        public string Ciudad { get; set; }
        [Required(ErrorMessage = "Ingrese un Pais ")]
        public string Pais { get; set; }

        public bool estaBloquedo()
        {
            if (this.LockoutEnd == null || this.LockoutEnd < DateTime.Now) 
            {
                return false;
            }
            else
            {
                if (this.LockoutEnd > DateTime.Now) { return true; }
            }
            return false;
           
        }

    }
}
