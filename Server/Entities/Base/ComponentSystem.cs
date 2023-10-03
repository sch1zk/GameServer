using Game.Server.Managers;

namespace Game.Server.Entities;

public class ComponentSystem<T>
    where T : Component
{
    public ComponentSystem()
    {
        GameTickManager.NextTick += async () => await DoTick();
    }

    protected static List<T> Components { get; private set; } = new List<T>();

    protected static void Register(T component)
    {
        Components.Add(component);
    }

    protected virtual void StartTick()
    {
        GameTickManager.IncrementCounter();
    }

    protected virtual void EndTick()
    {
        GameTickManager.DecrementCounter();
    }

    protected virtual async Task DoTick()
    {
        await Task.Yield();
    }
}