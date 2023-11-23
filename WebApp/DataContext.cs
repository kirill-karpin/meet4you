using Microsoft.EntityFrameworkCore;


namespace WebApp;

public class DataContext : DbContext
{
    public DbSet<User.User> Users { get; set; }

    public DataContext(DbContextOptions options) : base(options)
    {
    }
}