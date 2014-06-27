using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace WebApi.Web
{
    public class MyHttpHandler : IHttpHandler
    {
        bool IHttpHandler.IsReusable
        {
            get { return false; }
        }

        void IHttpHandler.ProcessRequest(HttpContext httpContext)
        {
            var request = httpContext.Request;
            Trace.TraceInformation("MyHttpHandler: {0}", request.Url);

            var context = new HttpContextWrapper(httpContext);
            var routeData = RouteTable.Routes.GetRouteData(context);
            if (routeData != null && routeData.RouteHandler != null)
            {
                Trace.TraceInformation("MyHttpHandler: Url: {0}, Route Handler: {1}", request.Url, routeData.RouteHandler.GetType().FullName);
            }
        }
    }
}