using Blog.AccesoDatos.Data.Repository;
using Blog.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.AccesoDatos.Data
{
    public class CateogoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private  readonly ApplicationDbContext _dbContext;

        public CateogoriaRepository(ApplicationDbContext context):base(context)
        {
           this._dbContext = context;
        }
        public IEnumerable<SelectListItem> GetListaCategorias()
        {
           return this. _dbContext.Cateogorias.Select(i => new SelectListItem(){ 
             Text = i.Nombre,
             Value = i.Id.ToString()
           });
        }

        public void Update(Categoria categoria)
        {
            var cat = this._dbContext.Cateogorias.FirstOrDefault(c => c.Id == categoria.Id);
            cat.Nombre = categoria.Nombre;
            cat.Orden = categoria.Orden;
            this._dbContext.SaveChanges();

        }
    }
}
