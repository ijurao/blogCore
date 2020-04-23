using Blog.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.AccesoDatos.Data.Repository
{
    public interface ISliderReposityry: IRepository<Slider> 
    {
        void Update(Slider articulo);

    }
}
