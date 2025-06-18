using DesafioClientes.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioClientes.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Endereco> Enderecos => Set<Endereco>();
    public DbSet<Contato> Contatos => Set<Contato>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>()
            .HasOne(c => c.Endereco)
            .WithOne()
            .HasForeignKey<Endereco>(e => e.ClienteID);

        modelBuilder.Entity<Cliente>()
            .HasMany(c => c.Contatos)
            .WithOne()
            .HasForeignKey(c => c.ClienteID);
    }
}
