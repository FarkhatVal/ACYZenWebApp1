using Microsoft.EntityFrameworkCore;

namespace ACYZenWebApp1.Models;

public class DZenActionContext : DbContext
{
    public DbSet<ZenAction> ZenActions { get; set; }
    public DbSet<Channel> Channels { get; set; }
 
    public DZenActionContext(DbContextOptions<DZenActionContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}