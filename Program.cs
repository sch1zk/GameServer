using Serilog;
using Game.Server.Managers;
using Game.Server.SignalR;

namespace Game.Server;

public class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();
        Log.Information("Logger was loaded successfully.");
        var host = CreateHostBuilder(args).Build();
        var hcts = new CancellationTokenSource();
        await host.StartAsync(hcts.Token);

        try
        {
            GameManager.StartGame();
        }
        catch
        {
            Log.Error("! Error while loading game!");
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
            return;
        }

        ReadConsole(hcts);
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }

    private static void ReadConsole(CancellationTokenSource hostCancellationTokenSource)
    {
        bool isRunning = true;

        while (isRunning)
        {
            string? userInput = Console.ReadLine();
            switch (userInput)
            {
                case "exit":
                    hostCancellationTokenSource.Cancel();
                    Environment.Exit(-1);
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;
            }
        }
    }
}