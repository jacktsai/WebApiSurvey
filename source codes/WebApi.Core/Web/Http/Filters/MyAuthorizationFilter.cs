using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Filters;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Threading;
using System.Net.Http.Headers;
using System.Net;
using WebApi.Web.Http.Controllers;

namespace WebApi.Web.Http.Filters
{
    internal class MyAuthorizationFilter : IAuthorizationFilter
    {
        Task<HttpResponseMessage> IAuthorizationFilter.ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var query = actionContext.Request.GetQueryNameValuePairs();
            foreach (var pair in query)
            {
                if (pair.Key == "pass" && pair.Value != "true")
                {
                    var response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    var completionSource = new TaskCompletionSource<HttpResponseMessage>();
                    completionSource.SetResult(response);
                    return completionSource.Task;
                }
            }

            return continuation();
        }

        bool IFilter.AllowMultiple
        {
            get { return true; }
        }
    }
}
