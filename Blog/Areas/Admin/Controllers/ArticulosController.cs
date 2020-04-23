using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blog.AccesoDatos.Data.Repository;
using Blog.Modelos.ViewModels;
using Blog.Modelos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Blog.Modelos.Utilidades;

namespace Blog.Areas.Admin
{
    [Authorize(Roles = Constantes.admin)]

    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _host;


        public ArticulosController(IContenedorTrabajo container, IWebHostEnvironment webHost )
        {
            this._contenedorTrabajo = container;
            _host = webHost;
        }
        public IActionResult Index()
        {
          
           
            return View();


        }
        [HttpGet]
        public IActionResult Create()
        {

            ArticuloVM avm = new ArticuloVM()
            {
                Articulo = new Modelos.Articulo(),
                Categorias = this._contenedorTrabajo.UTCategoria.GetListaCategorias()


            };
            return View(avm);


        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

         ArticuloVM  avm = new ArticuloVM()
            {
                Articulo = new Modelos.Articulo(),
                Categorias = this._contenedorTrabajo.UTCategoria.GetListaCategorias()


            };
            if(id != null)
            {

                avm.Articulo = this._contenedorTrabajo.UTArticulo.Get(id.GetValueOrDefault());
                return View(avm);

            }
            else
            {
                return NotFound("Contenido no encontrado");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM avm)
        {
            if(ModelState.IsValid)
            {

                string rutaPrincipal = _host.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (avm.Articulo.Id == 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"articulosImagenes");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    using (var fileStrean = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStrean);
                    }
                    avm.Articulo.UrlImagen = @"\articulosImagenes\" + nombreArchivo + extension;
                    avm.Articulo.FechaCreacion = DateTime.Now.ToString();
                    _contenedorTrabajo.UTArticulo.Add(avm.Articulo);
                    _contenedorTrabajo.Save();
                   return  RedirectToAction(nameof(Index));


                }
                
            }


            return View(avm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloVM avm)
        {
            if (ModelState.IsValid)
            {

                string rutaPrincipal = _host.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                var articulo = _contenedorTrabajo.UTArticulo.Get(avm.Articulo.Id);
                if (archivos.Count() > 0)
                {

                    // editamos archivo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"articulosImagenes");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    using (var fileStrean = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStrean);
                    }
                    avm.Articulo.UrlImagen = @"\articulosImagenes\" + nombreArchivo + extension;
               


                }
                else
                {
                    avm.Articulo.UrlImagen = articulo.UrlImagen;

                }
                avm.Articulo.FechaCreacion = DateTime.Now.ToString();

                _contenedorTrabajo.UTArticulo.Update(avm.Articulo);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));

            }


            return View(avm);

        }

        /// <summary>
        /// ////////////////apiii
        /// </summary>
        /// <returns></returns>
        #region AJAXAPI
        [HttpGet]
        public IActionResult GetAll()
        {

            return Json(new { data = this._contenedorTrabajo.UTArticulo.GetAll(includeProperties: "Categoria") });

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Articulo art = this._contenedorTrabajo.UTArticulo.Get(id);
            if (art == null)
            {

                return Json(new { success = false, message = "Error borrando el articulo" });
            }
            else
            {
                this._contenedorTrabajo.UTArticulo.Remove(art);
                _contenedorTrabajo.Save();
                return Json(new { success = true, message = "Articulo borrado!" });

            }


        }


        #endregion
    }
}