using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ikt201_Sultan_side.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Ikt201_Sultan_side.Models.Person> Personer { get; set; }
    public DbSet<Ikt201_Sultan_side.Models.Egenskaper> Egenskaper { get; set; }
    public DbSet<Ikt201_Sultan_side.Models.Bord> Bord { get; set; }
    public DbSet<Ikt201_Sultan_side.Models.Booking> Bookinger { get; set; }
    public DbSet<Ikt201_Sultan_side.Models.Kategori> Kategorier { get; set; }
    public DbSet<Ikt201_Sultan_side.Models.Rett> Retter { get; set; }
    public DbSet<Ikt201_Sultan_side.Models.RetterEgenskaper> RetterEgenskaper { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Ikt201_Sultan_side.Models.Booking>()
            .HasOne<Ikt201_Sultan_side.Models.Person>()
            .WithMany()
            .HasForeignKey(b => b.PersonId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Ikt201_Sultan_side.Models.Booking>()
            .HasOne<Ikt201_Sultan_side.Models.Person>()
            .WithMany()
            .HasForeignKey(b => b.BekreftetAdminId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Ikt201_Sultan_side.Models.Booking>()
            .HasOne<Ikt201_Sultan_side.Models.Bord>()
            .WithMany()
            .HasForeignKey(b => b.BordId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}