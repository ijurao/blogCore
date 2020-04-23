using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.AccesoDatos.Data.Repository;
using Blog.Modelos;
using Blog.Modelos.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    [Authorize(Roles = Constantes.admin)]

    [Area("Admin")]
    public class CategoriasController : Controller
    {

        private readonly IContenedorTrabajo _contenedorTrabajo;

        
        public CategoriasController(IContenedorTrabajo contenedor)
        {
            _contenedorTrabajo = contenedor;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        #region
        //llamas a la api
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json( new { data = this._contenedorTrabajo.UTCategoria.GetAll() });
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Categoria c = _contenedorTrabajo.UTCategoria.Get(id);
            if (c != null)
            {
                return View(c);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Create(Categoria cat)
        {
            if(ModelState.IsValid)
            {
                this._contenedorTrabajo.UTCategoria.Add(cat);
                this._contenedorTrabajo.Save();
                return (RedirectToAction(nameof(Index)));

            }
            return View(cat);

        }

        [HttpPost]
        public IActionResult Edit(Categoria cat)
        {
            if (ModelState.IsValid)
            {
                this._contenedorTrabajo.UTCategoria.Update(cat);
                this._contenedorTrabajo.Save();
                return (RedirectToAction(nameof(Index)));

            }
            return View(cat);

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
           Categoria cat = _contenedorTrabajo.UTCategoria.Get(id);
            if (cat == null)
            {

                return Json(new { success = false,message = "Error borrando la categoria" });
            }
            else
            {
                _contenedorTrabajo.UTCategoria.Remove(cat);
                _contenedorTrabajo.Save();
                return Json(new { success = true, message = "Categoria borrada!" });

            }
           

        }


        #endregion


    }
}