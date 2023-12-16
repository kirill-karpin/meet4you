using Microsoft.EntityFrameworkCore;

namespace Install;

public class DataContext : DbContext
{
    
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    
    public DbSet<Message.Message> Messages { get; set; }
    public DbSet<User.User> Users { get; set; }
}