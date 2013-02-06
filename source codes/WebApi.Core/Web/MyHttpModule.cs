using System;
using System.Diagnostics;
using System.Web;
using System.Web.Routing;

namespace WebApi.Web
{
    internal class MyHttpModule : IHttpModule
    {
        private HttpApplication httpApp;

        void IHttpModule.Init(HttpApplication httpApp)
        {
            this.httpApp = httpApp;
            this.httpApp.BeginRequest += HttpApplication_BeginRequest;
            this.httpApp.AuthenticateRequest += HttpApplication_AuthenticateRequest;
            this.httpApp.PostResolveRequestCache += HttpApplication_PostResolveRequestCache;
        }

        void IHttpModule.Dispose()
        {
            this.httpApp.BeginRequest -= HttpApplication_BeginRequest;
            this.httpApp.AuthenticateRequest -= HttpApplication_AuthenticateRequest;
            this.httpApp.PostResolveRequestCache -= HttpApplication_PostResolveRequestCache;
        }

        private void HttpApplication_BeginRequest(object sender, EventArgs e)
        {
            var request = httpApp.Request;
            Trace.WriteLine(string.Format("{0}", request.Url));

            var context = new HttpContextWrapper(httpApp.Context);
            var routeData = RouteTable.Routes.GetRouteData(context);
            if (routeData != null && routeData.RouteHandler != null)
            {
                Trace.WriteLine(string.Format("Url: {0}, Route Handler: {1}", request.Url, routeData.RouteHandler.GetType().FullName));
            }
        }

        private void HttpApplication_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        private void HttpApplication_PostResolveRequestCache(object sender, EventArgs e)
        {
        }
    }
}