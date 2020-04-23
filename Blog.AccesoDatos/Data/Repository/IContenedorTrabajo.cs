using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.AccesoDatos.Data.Repository
{
    public interface IContenedorTrabajo : IDisposable
    {

        // this class contain ALL repositories
        public ICategoriaRepository UTCategoria { get;  }
        public IArticuloRepository UTArticulo { get; }
        public ISliderReposityry UTSlider { get; }

        public IUsuarioRepositorio UTUsuario { get; set; }
        public void Save()
        {

        }

        
    }
}
