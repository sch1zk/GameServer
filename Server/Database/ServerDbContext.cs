using Game.Server.Database.PG_Objects;
using Game.Server.Managers;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Game.Server.Database;

public class ServerDbContext : DbContext
{
    public ServerDbContext()
    {
        try
        {
            Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            Log.Error("! Error while connecting to game database!");
            Log.Error($"! Err: {ex.Message}");
            throw;
        }
    }

    public DbSet<PG_PC> Players { get; set; } = null!;

    public DbSet<PG_Monster> Planets { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConfigManager.GetDbUseString());
    }
}