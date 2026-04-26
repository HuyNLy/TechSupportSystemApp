using Microsoft.EntityFrameworkCore;
using TechSupportSystemApp.Models;

namespace TechSupportSystemApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // M-M: Ticket <-> Category
    modelBuilder.Entity<Ticket>()
        .HasMany(t => t.Categories)
        .WithMany(c => c.Tickets);

    // 1-M: Employee -> Tickets
    modelBuilder.Entity<Employee>()
        .HasMany(e => e.Tickets)
        .WithOne(t => t.Employee)
        .HasForeignKey(t => t.EmployeeId);

    // -------------------------
    // SEEDING (MATCHES YOUR MODELS)
    // -------------------------

    modelBuilder.Entity<Employee>().HasData(
        new Employee { EId = 1, EName = "Alice" },
        new Employee { EId = 2, EName = "Bob" }
    );

    modelBuilder.Entity<Category>().HasData(
        new Category { CatId = 1, CatName = "Hardware" },
        new Category { CatId = 2, CatName = "Software" }
    );
}

}
