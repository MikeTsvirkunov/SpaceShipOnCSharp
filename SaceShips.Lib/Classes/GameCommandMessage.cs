using System.Collections.Generic;
using System.Runtime.Serialization;
using CoreWCF.OpenApi.Attributes;
using SaceShips.Lib.Interfaces;

namespace SaceShips.Lib.Classes;

public class GameCommandMessage: IGameIdContainer, IArgContainer, ICommandNameContainer
{
    [DataMember(Name = "command", Order = 1)]
    [OpenApiProperty(Description = "command description.")]
    public string? command_name { get; set; }

    [DataMember(Name = "game_id", Order = 2)]
    [OpenApiProperty(Description = "game_id description.")]
    public string? game_id { get; set; }

    [DataMember(Name = "args", Order = 3)]
    [OpenApiProperty(Description = "args description.")]
    public List<string>? args { get; set; }
}
