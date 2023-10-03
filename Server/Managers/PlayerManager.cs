using Serilog;
using Game.Server.Entities;

namespace Game.Server.Managers;

public static class PlayerManager
{
    private static readonly HashSet<Player> Players;

    static PlayerManager()
    {
        Log.Information("- Initializing PlayerManager...");
        Players = new HashSet<Player>();
    }

    public static void Initialize()
    {
    }

    public static async Task AddPlayer(Guid uuid)
    {
        var pgPlayer = await DatabaseManager.PullPlayerFromDbAsync(uuid);
    }

}