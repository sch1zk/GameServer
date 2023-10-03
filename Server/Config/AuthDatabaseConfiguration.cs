public sealed class AuthDatabaseConfiguration
{
    required public string Host { get; set; }

    required public ushort Port { get; set; }

    required public string Database { get; set; }

    required public string Username { get; set; }

    required public string Password { get; set; }
}