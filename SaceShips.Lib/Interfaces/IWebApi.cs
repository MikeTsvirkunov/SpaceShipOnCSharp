using System.Net;
using CoreWCF;
using CoreWCF.OpenApi.Attributes;
using CoreWCF.Web;

namespace SaceShips.Lib.Interfaces;

[ServiceContract]
[OpenApiBasePath("/api")]
public interface IWebApi
{
    [OperationContract]
    [WebInvoke(Method = "POST", UriTemplate = "/body")]
    [OpenApiTag("Tag")]
    [OpenApiResponse(ContentTypes = new[] { "application/json", "text/xml" }, 
                                            Description = "Success", 
                                            StatusCode = HttpStatusCode.OK, 
                                            Type = typeof(IMessage))]
    object get_message(
        [OpenApiParameter(ContentTypes = new[] { "application/json", "text/xml" }, Description = "param description.")]
        IMessage param);
}
