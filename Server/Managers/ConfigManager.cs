using Serilog;

namespace Game.Server.Managers;

public static class ConfigManager
{
    private const string ConfigFileName = "config.json";
    private static readonly AuthDatabaseConfiguration? AuthDbConfig;
    private static readonly DatabaseConfiguration? DbConfig;

    static ConfigManager()
    {
        Log.Information("- Initializing ConfigManager...");

        try
        {
            string path = ResourcesManager.ResourcesPath + ConfigFileName;
            Log.Information("] Loading config...");
            var configRoot = new ConfigurationBuilder()
                .AddJsonFile(path, false, false)
                .Build();

            GetConfigSection(configRoot, out AuthDbConfig);
            GetConfigSection(configRoot, out DbConfig);

            Log.Information($"] \'{path}\' was loaded.");
        }
        catch (Exception ex)
        {
            Log.Error("! Error while loading config file!");
            Log.Error($"! Err: {ex.Message}");
            throw;
        }
    }

    public static void Initialize()
    {
    }

    public static string GetDbUseString()
    {
        return $"Host={DbConfig?.Host};Port={DbConfig?.Port};Database={DbConfig?.Database};Username={DbConfig?.Username};Password={DbConfig?.Password}";
    }

    public static string GetAuthDbUseString()
    {
        return $"Host={AuthDbConfig?.Host};Port={AuthDbConfig?.Port};Database={AuthDbConfig?.Database};Username={AuthDbConfig?.Username};Password={AuthDbConfig?.Password}";
    }

    private static void GetConfigSection<TConfiguration>(IConfigurationRoot configRoot, out TConfiguration? config)
    {
        config = configRoot.GetRequiredSection(typeof(TConfiguration).Name).Get<TConfiguration>();
    }
}