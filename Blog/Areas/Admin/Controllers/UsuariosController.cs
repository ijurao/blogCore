using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.AccesoDatos.Data.Repository;
using Blog.Modelos.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    [Authorize(Roles = Constantes.admin)]
    [Area("Admin")]
    public class UsuariosController : Controller
    {

        private readonly IContenedorTrabajo _contenedorTrabajo;


        public UsuariosController(IContenedorTrabajo contenedor)
        {
            _contenedorTrabajo = contenedor;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return View(_contenedorTrabajo.UTUsuario.GetAll(u => u.Id != usuarioActual.Value));
        }

        [HttpGet]
        public IActionResult Bloquear(string id)
        {
            if(id == null)
            {
                return NotFound();
            }
            else
            {
                _contenedorTrabajo.UTUsuario.bloquearUsuario(id);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Desbloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                _contenedorTrabajo.UTUsuario.desbloquearUsuario(id);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}