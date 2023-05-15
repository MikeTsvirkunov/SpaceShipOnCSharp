using System.Net;
using CoreWCF;
using CoreWCF.OpenApi.Attributes;
using CoreWCF.Web;
using SaceShips.Lib.Classes;

namespace SaceShips.Lib.Interfaces;

[ServiceContract]
[OpenApiBasePath("/game_command_post")]
public interface IGameCommandEndPoint
{
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/body")]
    [OpenApiTag("Tag")]
    [OpenApiResponse(ContentTypes = new[] { "application/json", "text/xml" }, 
                                            Description = "Success", 
                                            StatusCode = HttpStatusCode.OK, 
                                            Type = typeof(GameCommandMessage))]
    object get_message(
        [OpenApiParameter(ContentTypes = new[] { "application/json", "text/xml" }, Description = "GameCommand unpack.")]
        GameCommandMessage param);
}
