using Microsoft.EntityFrameworkCore;

namespace ACYZenWebApp1.Models;

public class ACYZenWebAppContext : DbContext
{
    public DbSet<ZenAction> ZenActions { get; set; }
    public DbSet<Channel> Channels { get; set; }
 
    public ACYZenWebAppContext(DbContextOptions<ACYZenWebAppContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public ACYZenWebAppContext()
    {
        Database.EnsureCreated();
    }
}