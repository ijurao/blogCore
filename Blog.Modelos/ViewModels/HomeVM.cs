using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Modelos.ViewModels
{
    public class HomeVM
    {
        // esta vista modelo mestra todo lo del inicio (sliders y articulos)
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Articulo> Articulos { get; set; }

    }
}
