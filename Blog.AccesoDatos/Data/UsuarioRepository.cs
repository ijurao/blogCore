using Blog.AccesoDatos.Data.Repository;
using Blog.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.AccesoDatos.Data
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepositorio
    {
        private readonly ApplicationDbContext _dbContext;

        public UsuarioRepository(ApplicationDbContext context):base(context)
        {
            _dbContext = context;
        }

        public void bloquearUsuario(string idUsuario)
        {
            var user = this._dbContext.Usuarios.FirstOrDefault(u => u.Id == idUsuario);
            if(user != null)
            {
                user.LockoutEnd = DateTime.Now.AddYears(100);
                _dbContext.SaveChanges();
            }
        }

        public void desbloquearUsuario(string idUsuario)
        {
            var user = this._dbContext.Usuarios.FirstOrDefault(u => u.Id == idUsuario);
            if (user != null)
            {
                user.LockoutEnd = DateTime.Now;
                _dbContext.SaveChanges();
            }
        }

  
    }
}
