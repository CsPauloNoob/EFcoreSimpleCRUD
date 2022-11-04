using System;
using Dominio;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Dados
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

    //Aplicar LazyLoading, que tras objetos referenciados de outras tabelas
    //para o objeto atual. Produto.Categoria(...)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseLazyLoadingProxies();
    }


    //Exemplo de Fluent API, alteração durante a formação do DB, aplicar um
    //migration para aplicar.

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>().ToTable("Produto");
        modelBuilder.Entity<Produto>().Property(p => p.Nome).HasMaxLength(50);
        modelBuilder.Entity<Pedido>().HasKey(p => p.Numero);
    }

        public DbSet<Categoria> Categorias {get; set;}

        public DbSet<Produto> Produtos {get; set;}

        public DbSet<Pedido> Pedido {get; set;}
    }
}