using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace WebApi.Web.Routing
{
    public class MyUrlRoutingHandler : UrlRoutingHandler
    {
        protected override void VerifyAndProcessRequest(IHttpHandler httpHandler, HttpContextBase httpContext)
        {
            throw new NotImplementedException();
        }
    }
}