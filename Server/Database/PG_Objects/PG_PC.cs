using System.ComponentModel.DataAnnotations;
using NpgsqlTypes;

namespace Game.Server.Database.PG_Objects;

public class PG_PC
{
    [Key]
    public Guid Uuid { get; set; }

    required public string Name { get; set; }

    public Guid UserUuid { get; set; }

    public NpgsqlPoint CurrentPosition { get; set; }
}
