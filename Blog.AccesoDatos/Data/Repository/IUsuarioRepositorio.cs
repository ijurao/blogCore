using Blog.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.AccesoDatos.Data.Repository
{
    public interface IUsuarioRepositorio : IRepository<ApplicationUser> 
    {
        void bloquearUsuario(string idUsuario);
        void desbloquearUsuario(string idUsuario);

    }
}
