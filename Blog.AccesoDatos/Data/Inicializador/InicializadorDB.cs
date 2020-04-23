using Blog.Modelos;
using Blog.Modelos.Utilidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.AccesoDatos.Data.Inicializador
{
    public class InicializadorDB : IInicializadorDB
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolManager;
        public InicializadorDB(ApplicationDbContext  db, UserManager<ApplicationUser> um, RoleManager<IdentityRole> rolManager)
        {
            _context = db;
            _userManager = um;
            _rolManager = rolManager;
        }
        public void inicializar()
        {
            try
            {
                if(_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {

                throw;
            }
            if(_context.Roles.Any(ro => ro.Name == Constantes.admin) )
            {
                return;
            }

            _rolManager.CreateAsync(new IdentityRole(Constantes.admin)).Wait();// no es aconsejable por el tipo de exepciones
            _rolManager.CreateAsync(new IdentityRole(Constantes.cliente)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@local.com",
                Email = "admin@local.com",
                EmailConfirmed = true,
                Nombre = "Nacho"

            },"Admin123!").GetAwaiter().GetResult();


             ApplicationUser user = _context.Usuarios.Where(u => u.Email == "admin@local.com").FirstOrDefault();
            _userManager.AddToRoleAsync(user, Constantes.admin);
        }
    }
}
