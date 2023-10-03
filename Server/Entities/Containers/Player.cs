using Game.Server.Database.PG_Objects;

namespace Game.Server.Entities;

public class Player : Entity
{
    public Player(PG_PC pg_player)
    {
        Uuid = pg_player.Uuid;
        Name = pg_player.Name;
        AddComponents(
            new PositionComponent(pg_player.CurrentPosition));
    }
}