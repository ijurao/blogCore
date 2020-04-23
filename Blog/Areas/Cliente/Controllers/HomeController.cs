using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Blog.AccesoDatos.Data.Repository;
using Blog.Modelos;
using Blog.Modelos.ViewModels;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blog.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {


        private readonly IContenedorTrabajo _contenedorTrabajo;
        public HomeController(IContenedorTrabajo contenedor)
        {
            _contenedorTrabajo = contenedor;
        }


        public IActionResult Index()
        {
            HomeVM homeVm = new HomeVM()
            {
                Sliders = _contenedorTrabajo.UTSlider.GetAll(),
                Articulos = _contenedorTrabajo.UTArticulo.GetAll(),
            };
            ;
            return View(homeVm);
        }

        public IActionResult Details(int id)
        {
            Articulo a = this._contenedorTrabajo.UTArticulo.GetFirstOrDefault(a => a.Id == id);
            return View(a);

        }



    }
}
