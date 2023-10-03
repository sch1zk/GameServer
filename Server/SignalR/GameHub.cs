using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Serilog;
using Game.Server.Managers;

namespace Game.Server.SignalR;

[Authorize]
public class GameHub : Hub
{
}