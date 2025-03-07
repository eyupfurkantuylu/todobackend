using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.API.Models.Identity;

namespace TodoApp.API.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<TodoItems> TodoItems { get; set; }
    public DbSet<TodoList> TodoLists { get; set; }
    public DbSet<Settings> Settings { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // TodoList - User ilişkisi
        builder.Entity<TodoList>()
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey("UserId")
            .IsRequired();

        // TodoItems - TodoList ilişkisi
        builder.Entity<TodoItems>()
            .HasOne<TodoList>()
            .WithMany()
            .HasForeignKey(t => t.TodoListId)
            .IsRequired();

        // Settings - User ilişkisi
        builder.Entity<Settings>()
            .HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<Settings>("UserId")
            .IsRequired();
    }
} 
