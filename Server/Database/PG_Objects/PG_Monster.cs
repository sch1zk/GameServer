using System.ComponentModel.DataAnnotations;
using NpgsqlTypes;

namespace Game.Server.Database.PG_Objects;

public class PG_Monster
{
    [Key]
    public Guid Uuid { get; set; }

    public string? Name { get; set; }

    public NpgsqlPoint CurrentPosition { get; set; }
}