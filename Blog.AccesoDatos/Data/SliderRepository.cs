using Blog.AccesoDatos.Data.Repository;
using Blog.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.AccesoDatos.Data
{
    public class SliderRepository : Repository<Slider>, ISliderReposityry
    {
        private readonly ApplicationDbContext _dbContext;

        public SliderRepository(ApplicationDbContext context):base(context)
        {
            _dbContext = context;
        }
      

        public void Update(Categoria categoria)
        {
            
        }

        public void Update(Slider sldParam)
        {
            var sld = this._dbContext.Sliders.FirstOrDefault(c => c.Id == sldParam.Id);
            sld.Titulo = sldParam.Titulo;
            sld.FechaCreacion = sldParam.FechaCreacion;
            sld.UrlImagen = sldParam.UrlImagen;
            this._dbContext.SaveChanges();
        }
    }
}
