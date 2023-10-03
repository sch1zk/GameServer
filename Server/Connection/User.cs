using Game.Server.Database.PG_Auth;

namespace Game.Server.Connection;

public class User
{
    public User(PG_User user)
    {
        Uuid = user.Uuid;
        Name = user.Name;
    }

    public Guid Uuid { get; private set; }

    public string Name { get; private set; }
}
