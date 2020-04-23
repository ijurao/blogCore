using System;
using System.Collections.Generic;
using System.Text;
using Blog.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.AccesoDatos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Cateogorias{get; set;}
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<ApplicationUser> Usuarios { get; set; }



    }

}
