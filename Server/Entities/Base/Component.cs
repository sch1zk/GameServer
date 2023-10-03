namespace Game.Server.Entities;

public abstract class Component
{
    protected Entity? Container { get; set; }
}