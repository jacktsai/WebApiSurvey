using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Web.Http.SelfHost;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Threading;
using WebApi.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Web.Http;
using WebApi.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Net;
using System.Web.Http.Hosting;
using System.Web.Http.WebHost;

namespace WebApi.Web.Http.Filters
{
    [TestClass]
    public class MyAuthorizationFilterTests
    {
        [TestMethod]
        public void AuthorizeTest()
        {
            var httpConfiguration = new HttpConfiguration();

            var httpRoute = httpConfiguration.Routes.MapHttpRoute("API Default", "api/{controller}/{action}");

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8080/api/values/get");
            var routeData = httpRoute.GetRouteData(string.Empty, request);
            request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);

            var httpControllerSelector = httpConfiguration.Services.GetHttpControllerSelector();
            var controllerDescriptor = httpControllerSelector.SelectController(request);
            var controllerContext = new HttpControllerContext(httpConfiguration, routeData, request);

            var actionSelector = httpConfiguration.Services.GetActionSelector();
            var actionDescriptor = actionSelector.SelectAction(controllerContext);
            var actionContext = new HttpActionContext(controllerContext, actionDescriptor);

            var target = new MyAuthorizationFilter() as IAuthorizationFilter;
            target.ExecuteAuthorizationFilterAsync(actionContext, CancellationToken.None, () =>
                {
                    var source = new TaskCompletionSource<HttpResponseMessage>();
                    source.SetResult(new HttpResponseMessage(HttpStatusCode.OK));
                    return source.Task;
                });
        }
    }
}
