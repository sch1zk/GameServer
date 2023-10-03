using Serilog;

namespace Game.Server.Managers;

public static class GameTickManager
{
    private static PeriodicTimer tickTimer = new (TimeSpan.FromMilliseconds(50));

    private static CancellationTokenSource cts = new ();

    private static int subscribedToTickCounter;

    static GameTickManager()
    {
        Log.Information("- Initializing GameTickManager...");
    }

    public delegate void NextTickHandler();

    public static event NextTickHandler? NextTick;

    public static void Initialize()
    {
    }

    public static void StartTick()
    {
        Log.Information("] Starting GameTick...");
        _ = GameTick();
    }

    public static void StopTick()
    {
        Log.Warning("] GameTick stopped!");
        cts.Cancel();
    }

    public static void IncrementCounter()
    {
        subscribedToTickCounter++;
    }

    public static void DecrementCounter()
    {
        subscribedToTickCounter--;
    }

    private static async Task GameTick()
    {
        while (await tickTimer.WaitForNextTickAsync(cts.Token))
        {
            if (subscribedToTickCounter > 0)
            {
                continue;
            }

            subscribedToTickCounter = 0;
            NextTick?.Invoke();
        }
    }
}