using System;
using System.Collections.Generic;
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
            var context = new HttpContextWrapper(httpContext);
            var routeData = RouteTable.Routes.GetRouteData(context);
        }
    }
}
