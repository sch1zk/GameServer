using Microsoft.EntityFrameworkCore;
using Serilog;
using Game.Server.Database.PG_Auth;
using Game.Server.Managers;

namespace Game.Server.Database;

public class AuthDbContext : DbContext
{
    public AuthDbContext()
    {
        try
        {
            Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            Log.Error("! Error while connecting to auth database!");
            Log.Error($"! Err: {ex.Message}");
            throw;
        }
    }

    public DbSet<PG_User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConfigManager.GetAuthDbUseString());
    }
}