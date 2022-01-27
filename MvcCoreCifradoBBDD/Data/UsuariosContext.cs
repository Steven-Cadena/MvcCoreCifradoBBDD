using Microsoft.EntityFrameworkCore;
using MvcCoreCifradoBBDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCifradoBBDD.Data
{

    public class UsuariosContext:DbContext
    {
        public UsuariosContext(DbContextOptions<UsuariosContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
