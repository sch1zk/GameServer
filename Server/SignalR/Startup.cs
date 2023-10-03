using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Game.Server.Database.PG_Auth;
using Game.Server.Managers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Game.Server.SignalR;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSignalR();
        services.AddAuthorization();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AuthOptions.AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/game"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    },
                };
            });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapPost("/auth/login", async (LoginModel loginModel) =>
            {
                Log.Debug("Login request was made.");
                PG_User? user = await AuthDbManager.VerifyUserByDbAsync(loginModel);
                if (user == null)
                {
                    return Results.Unauthorized();
                }

                var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Name) };
                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var response = new
                {
                    access_token = encodedJwt,
                };
                Log.Debug("Login request accepted. Sending response.");
                return Results.Json(response);
            });
            endpoints.MapPost("/auth/register", async (LoginModel loginModel) =>
            {
                Log.Information("Register request was made.");
                if (!await AuthDbManager.CheckNameInDbAsync(loginModel.name))
                {
                    Log.Information("Register request accepted. Creating new user.");
                    await AuthDbManager.PushNewUserToDbAsync(loginModel);
                }
            });
            endpoints.MapHub<GameHub>("/game");
        });
    }
}

public record class LoginModel(string name, string password);