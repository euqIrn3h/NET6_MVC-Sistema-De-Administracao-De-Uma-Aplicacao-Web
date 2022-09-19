using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Admin_MVC.Models;

namespace Admin_MVC.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options): base(options)
        {
        }

        public DbSet<UsuarioData>? Usuarios { get; set; }
        public DbSet<ProdutoData>? Produtos { get; set; }
        public DbSet<CategoriaData>? Categorias { get; set; }
        public DbSet<TermoData>? Termos { get; set; }
    }
}