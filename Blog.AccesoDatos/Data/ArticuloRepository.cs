using Blog.AccesoDatos.Data.Repository;
using Blog.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.AccesoDatos.Data
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ArticuloRepository(ApplicationDbContext context):base(context)
        {
            _dbContext = context;
        }
      

        public void Update(Categoria categoria)
        {
            
        }

        public void Update(Articulo articulo)
        {
            var art = this._dbContext.Articulos.FirstOrDefault(c => c.Id == articulo.Id);
            art.Titulo = articulo.Titulo;
            art.Descripcion = articulo.Descripcion;
            art.FechaCreacion = articulo.FechaCreacion;
            art.UrlImagen = articulo.UrlImagen;
            this._dbContext.SaveChanges();
        }
    }
}
