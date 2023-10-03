using Game.Server.Database;
using Game.Server.Database.PG_Auth;
using Game.Server.SignalR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using BC = BCrypt.Net.BCrypt;

namespace Game.Server.Managers;

public static class AuthDbManager
{
    private static readonly AuthDbContext AuthDb;

    static AuthDbManager()
    {
        Log.Information("- Initializing AuthDbManager...");
        AuthDb = new AuthDbContext();
    }

    public static void Initialize()
    {
    }

    public static async Task<bool> CheckNameInDbAsync(string name)
    {
        return await AuthDb.Users.AnyAsync(u => u.Name.Equals(name));
    }

    public static async Task PushNewUserToDbAsync(LoginModel user)
    {
        string hashedPassword = BC.HashPassword(user.password);
        PG_User newUser = new PG_User(user.name, hashedPassword);
        AuthDb.Add(newUser);
        await AuthDb.SaveChangesAsync();
    }

    public static async Task<PG_User?> VerifyUserByDbAsync(LoginModel user)
    {
        var db_user = await AuthDb.Users.SingleOrDefaultAsync(s => s.Name.Equals(user.name));
        if (db_user != null && BC.Verify(user.password, db_user.Password))
        {
            return db_user;
        }

        return null;
    }

    public static async Task<PG_User?> PullUserFromDbAsync(string name)
    {
        PG_User? user;
        try
        {
            user = await AuthDb.Users
                .SingleOrDefaultAsync(u => u.Name == name);
        }
        catch (Exception ex)
        {
            Log.Error("! Error while pulling user from database!");
            Log.Error($"! Err: {ex.Message}");
            throw;
        }

        return user;
    }
}