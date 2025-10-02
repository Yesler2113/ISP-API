using ISP_API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ISP_API.Data;

public class AppDbContext : IdentityDbContext<UserEntity>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserEntity>().ToTable("users");
        builder.Entity<IdentityRole>().ToTable("roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("users_roles");
        builder.Entity<IdentityUserClaim<string>>().ToTable("users_claims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("users_logins");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("roles_claims");
        builder.Entity<IdentityUserToken<string>>().ToTable("users_tokens");
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<ClienteEntity> Clientes { get; set; }
    public DbSet<ClientePlanEntity> Planes { get; set; }
    public DbSet<EquipoClienteEntity> EquipoClientes { get; set; }
    public DbSet<EquipoEntity> Equipos { get; set; }
    public DbSet<PagoDetalleEntity> PagoDetalles { get; set; }
    public DbSet<PagoEntity> Pagos { get; set; }
    public DbSet<PlanEntity> PlanesServicio { get; set; }
}