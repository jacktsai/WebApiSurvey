using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Web;
using System;
using Newtonsoft.Json;
using WebApi.IO;

namespace WebApi.Net.Http
{
    internal class DumpMessageHandler : DelegatingHandler
    {
        private readonly FileRepository fileRep = new FileRepository();

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            fileRep.LogRequest(request);

            var task = base.SendAsync(request, cancellationToken);

            var response = task.Result;

            fileRep.LogResponse(request, response);

            return task;
        }
    }
}