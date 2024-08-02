using BackEnd.API.Modules;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.API.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>().HasKey(s => s.Id);

        modelBuilder.Entity<Student>()
            .Property(s => s.Name)
            .HasColumnType("varchar(50)")
            .IsRequired();

        modelBuilder.Entity<Student>()
            .Property(s => s.Address)
            .HasColumnType("varchar(50)")
            .IsRequired();

        modelBuilder.Entity<Student>()
            .Property(s => s.PhoneNumber)
            .HasColumnType("varchar(50)")
            .IsRequired();

        modelBuilder.Entity<Student>()
            .Property(s => s.Email)
            .HasColumnType("varchar(50)")
            .IsRequired();
    }
}
