using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blog.AccesoDatos.Data.Repository;
using Blog.Modelos;
using Blog.Modelos.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {

        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _host;


        public SlidersController(IContenedorTrabajo container, IWebHostEnvironment webHost)
        {
            this._contenedorTrabajo = container;
            _host = webHost;
        }
        public IActionResult Index()
        {


            return View();


        }

        public IActionResult Create()
        {
          
            return View();
            


        }
        [HttpPost]
        public IActionResult Create(Slider s)
        {
            
            if (ModelState.IsValid)
            {

                string rutaPrincipal = _host.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (s.Id == 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"sliders");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    using (var fileStrean = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStrean);
                    }
                    //s.UrlImagen = Path.Combine("sliders" , nombreArchivo , extension);
                    s.UrlImagen = @"\sliders\" + nombreArchivo + extension;

                    s.FechaCreacion = DateTime.Now.ToString();
                    _contenedorTrabajo.UTSlider.Add(s);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));


                }

            }


            return View(s);
          


        }



        #region AJAXAPI
        [HttpGet]
        public IActionResult GetAll()
        {

            return Json(new { data = this._contenedorTrabajo.UTSlider.GetAll() });

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Slider s = this._contenedorTrabajo.UTSlider.Get(id);
            if (s == null)
            {

                return Json(new { success = false, message = "Error borrando el slider" });
            }
            else
            {
                this._contenedorTrabajo.UTSlider.Remove(s);
                _contenedorTrabajo.Save();
                return Json(new { success = true, message = "Slider Borrado!" });

            }


        }



        #endregion

    }
}
