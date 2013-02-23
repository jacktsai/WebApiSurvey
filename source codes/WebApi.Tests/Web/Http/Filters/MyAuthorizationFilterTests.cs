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
using System.Web.Http.Routing;
using WebApi.Util;

namespace WebApi.Web.Http.Filters
{
    [TestClass]
    public class MyAuthorizationFilterTests
    {
        [TestMethod]
        public void WithoutPass()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8080/api/values/get");
            var controllerContext = ContextUtil.CreateControllerContext(request: request);
            var actionContext = ContextUtil.CreateActionContext(controllerContext: controllerContext);

            var expected = new HttpResponseMessage(HttpStatusCode.OK);

            var target = new MyAuthorizationFilter() as IAuthorizationFilter;
            var actual = target.ExecuteAuthorizationFilterAsync(actionContext, CancellationToken.None, () =>
                {
                    var source = new TaskCompletionSource<HttpResponseMessage>();
                    source.SetResult(expected);
                    return source.Task;
                }).Result;

            Assert.AreSame(expected, actual);
        }

        [TestMethod]
        public void PassIsTrue()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8080/api/values/get?pass=true");
            var controllerContext = ContextUtil.CreateControllerContext(request: request);
            var actionContext = ContextUtil.CreateActionContext(controllerContext: controllerContext);

            var expected = new HttpResponseMessage(HttpStatusCode.OK);

            var target = new MyAuthorizationFilter() as IAuthorizationFilter;
            var actual = target.ExecuteAuthorizationFilterAsync(actionContext, CancellationToken.None, () =>
            {
                var source = new TaskCompletionSource<HttpResponseMessage>();
                source.SetResult(expected);
                return source.Task;
            }).Result;

            Assert.AreSame(expected, actual);
        }

        [TestMethod]
        public void PassIsFalse()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:8080/api/values/get?pass=false");
            var controllerContext = ContextUtil.CreateControllerContext(request: request);
            var actionContext = ContextUtil.CreateActionContext(controllerContext: controllerContext);

            var expected = HttpStatusCode.Unauthorized;

            var target = new MyAuthorizationFilter() as IAuthorizationFilter;
            var actual = target.ExecuteAuthorizationFilterAsync(actionContext, CancellationToken.None, () =>
            {
                return new TaskCompletionSource<HttpResponseMessage>().Task;
            }).Result;

            Assert.AreEqual(expected, actual.StatusCode);
        }
    }
}
