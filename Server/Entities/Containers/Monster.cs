using Game.Server.Database.PG_Objects;

namespace Game.Server.Entities;

public class Monster : Entity
{
    public Monster(PG_Monster pg_planet)
    {
        Uuid = pg_planet.Uuid;
        Name = pg_planet.Name;
        AddComponents(
            new PositionComponent(pg_planet.CurrentPosition));
    }
}