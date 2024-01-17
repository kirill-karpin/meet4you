using Country;
using Microsoft.EntityFrameworkCore;

namespace Install;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }


    public DbSet<Message.Message> Messages { get; set; }
    public DbSet<User.User> Users { get; set; }
    public DbSet<Location.Country.Country> Countries { get; set; }
    public DbSet<Location.City.City> Cities { get; set; }
    public DbSet<Location.UserLocation.UserLocation> UserLocations { get; set; }
}