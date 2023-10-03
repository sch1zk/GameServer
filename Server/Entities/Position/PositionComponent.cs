using NpgsqlTypes;
using Game.Server.Structs;

namespace Game.Server.Entities;

public class PositionComponent : Component
{
    public PositionComponent(NpgsqlPoint position)
    {
        Position = new Coordinates(position.X, position.Y);
    }

    public Coordinates Position { get; set; }
}