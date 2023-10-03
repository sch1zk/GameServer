using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game.Server.Database.PG_Auth;

public class PG_User
{
    public PG_User(string name, string password)
    {
        Name = name;
        Password = password;
    }

    [Key]
    public Guid Uuid { get; private set; }

    public string Name { get; private set; }

    public string Password { get; private set; }
}
