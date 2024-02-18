using Microsoft.EntityFrameworkCore;
using Location.UserLocation;

namespace Install;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        // без этой строки у нас поля типа datetime будут создаваться с timezone, что рушит нафиг всю работу
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }


    public DbSet<Message.Message> Messages { get; set; }
    public DbSet<User.User> Users { get; set; }
    public DbSet<Location.Country.Country> Countries { get; set; }
    public DbSet<Location.City.City> Cities { get; set; }
    public DbSet<UserLocation> UserLocations { get; set; }
    public DbSet<User.ResetPassword> ResetPasswords { get; set; }
}