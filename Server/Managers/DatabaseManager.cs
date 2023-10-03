using Microsoft.EntityFrameworkCore;
using Serilog;
using Game.Server.Database;
using Game.Server.Database.PG_Objects;
using Game.Server.Entities;

namespace Game.Server.Managers;

public static class DatabaseManager
{
    private static readonly ServerDbContext Db;

    static DatabaseManager()
    {
        Log.Information("- Initializing DatabaseManager...");
        Db = new ServerDbContext();
    }

    public static void Initialize()
    {
    }

    public static Task<PG_PC?> PullPlayerFromDbAsync(Guid uuid)
    {
        return Db.Players.SingleOrDefaultAsync(u => u.UserUuid == uuid);
    }
}