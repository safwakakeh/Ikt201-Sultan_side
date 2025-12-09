using Ikt201_Sultan_side.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ikt201_Sultan_side.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    // Dine Admin-tabeller (Behold disse for at Admin skal virke nå)
    public DbSet<Dish> Dishes { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    // Nettsidens tabeller (MÅ være med for at nettsiden skal virke)
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

        // Dish configuration
        modelBuilder.Entity<Dish>().Property(d => d.Price).HasPrecision(18, 2);

        // Order configuration
        modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);

        modelBuilder.Entity<OrderItem>().Property(oi => oi.PriceAtOrder).HasPrecision(18, 2);

        // OrderItem relationships
        modelBuilder
            .Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder
            .Entity<OrderItem>()
            .HasOne(oi => oi.Dish)
            .WithMany()
            .HasForeignKey(oi => oi.DishId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .Entity<Ikt201_Sultan_side.Models.Booking>()
            .HasOne<Ikt201_Sultan_side.Models.Person>()
            .WithMany()
            .HasForeignKey(b => b.PersonId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder
            .Entity<Ikt201_Sultan_side.Models.Booking>()
            .HasOne<Ikt201_Sultan_side.Models.Person>()
            .WithMany()
            .HasForeignKey(b => b.BekreftetAdminId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder
            .Entity<Ikt201_Sultan_side.Models.Booking>()
            .HasOne<Ikt201_Sultan_side.Models.Bord>()
            .WithMany()
            .HasForeignKey(b => b.BordId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ikt201_Sultan_side.Models.Person>().HasIndex(p => p.Epost).IsUnique();

        modelBuilder.Entity<Ikt201_Sultan_side.Models.Person>().HasIndex(p => p.Telefon).IsUnique();
    }
}
