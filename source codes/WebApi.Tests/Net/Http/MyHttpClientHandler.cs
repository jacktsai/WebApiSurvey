using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Headers;

namespace WebApi.Net.Http
{
    /// <summary>
    /// A HttpMessageHandler which will set Authorization to headers.
    /// </summary>
    class MyHttpClientHandler : HttpClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Seven11", "Jack Tsai");
            return base.SendAsync(request, cancellationToken);
        }
    }
}
