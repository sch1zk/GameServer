namespace Game.Server.Entities;

public class Entity
{
    private readonly List<Component> components = new List<Component>();

    public Guid Uuid { get; set; }

    public string? Name { get; set; }

    public void AddComponents(params Component[] componentsToAdd)
    {
        foreach (var component in componentsToAdd)
        {
            components.Add(component);
        }
    }

    public T? GetComponent<T>()
        where T : Component
    {
        foreach (Component component in components)
        {
            if (component.GetType().Equals(typeof(T)))
            {
                return (T)component;
            }
        }

        return null;
    }
}