using Blog.AccesoDatos.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.AccesoDatos.Data
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _dbContext;
        public ContenedorTrabajo(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
            
           UTCategoria = new CateogoriaRepository(this._dbContext);//prestar atencion a esta linea para analizar despues, al final no es tan extensible
           UTSlider = new SliderRepository(this._dbContext);//prestar atencion a esta linea para analizar despues, al final no es tan extensible
           UTArticulo = new ArticuloRepository(this._dbContext);//prestar atencion a esta linea para analizar despues, al final no es tan extensible
            UTUsuario = new UsuarioRepository(this._dbContext);
        }
        public ICategoriaRepository UTCategoria {get; private set; }
        public IArticuloRepository UTArticulo{ get; private set; }
        public ISliderReposityry UTSlider{ get; private set; }

        public IUsuarioRepositorio UTUsuario { get; set; }

        public void Save()
        {
            this._dbContext.SaveChanges();
        }
        public void Dispose()
        {
            this._dbContext.Dispose();
        }


    }
}
