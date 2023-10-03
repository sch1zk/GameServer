using System.Diagnostics;
using Serilog;

namespace Game.Server.Managers;

public static class GameManager
{
    public static void StartGame()
    {
        Log.Information("Staring game...");
        Log.Information("- Initializing managers...");

        try
        {
            ConfigManager.Initialize();
            AuthDbManager.Initialize();
            DatabaseManager.Initialize();
            GameTickManager.Initialize();
            ComponentSystemsManager.Initialize();
            GameTickManager.StartTick();
        }
        catch
        {
            Log.Error("! Error while initializing managers!");
            throw;
        }

        Log.Information($"Game started!");
    }
}