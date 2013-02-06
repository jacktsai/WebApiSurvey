using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Web;
using System.Security.Principal;
using WebApi.BLL;
using WebApi.BLL.Authentication;
using System.Net;

namespace WebApi.Net.Http
{
    internal class AuthenticationHandler : DelegatingHandler
    {
        private readonly AuthenticationFactory factory = new AuthenticationFactory();

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool authenticated = false;

            if (request.Headers.Authorization != null)
            {
                //執行身份認證。
                IAuthentication auth = factory.GetAuthentication();
                authenticated = auth.Authenticate(request.Headers.Authorization);
            }

            //由於專案內仍有允許匿名存取的 API controller，所以不管授權。
            //if (!authenticated)
            //{
            //    var response = request.CreateResponse(HttpStatusCode.Unauthorized);
            //    var completionSource = new TaskCompletionSource<HttpResponseMessage>();
            //    completionSource.SetResult(response);
            //    return completionSource.Task;
            //}

            return base.SendAsync(request, cancellationToken);
        }
    }
}
