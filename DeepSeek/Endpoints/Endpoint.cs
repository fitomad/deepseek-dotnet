using System.Net;
using Fitomad.DeepSeek;

namespace Fitomad.DeepSeek.Endpoints;

public class Endpoint
{
    protected internal DeepSeekException.Failure ProcessHttpStatus(HttpStatusCode responseStatus)
    {
        var statusCode = (int) responseStatus;
        
        if (Enum.IsDefined(typeof(DeepSeekException.Failure), statusCode))
        {
            return (DeepSeekException.Failure) statusCode;
        }
        
        return DeepSeekException.Failure.Unknown;
    }
}