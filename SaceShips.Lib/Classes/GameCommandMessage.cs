using System.Collections.Generic;
using System.Runtime.Serialization;
using CoreWCF.OpenApi.Attributes;
using SaceShips.Lib.Interfaces;

namespace SaceShips.Lib.Classes;

public class GameCommandMessage: IMessage
{
    [DataMember(Name = "command", Order = 1)]
    [OpenApiProperty(Description = "SimpleProperty description.")]
    public string? command { get; set; }

    [DataMember(Name = "game_id", Order = 2)]
    [OpenApiProperty(Description = "game_id description.")]
    public string? game_id { get; set; }

    [DataMember(Name = "args", Order = 3)]
    [OpenApiProperty(Description = "args description.")]
    public List<string>? args { get; set; }
}
