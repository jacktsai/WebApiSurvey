using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Tracing;
using System.Web.Http;

namespace WebApi.Web.Http.Tracing
{
    class MyTraceManager : ITraceManager
    {
        void ITraceManager.Initialize(HttpConfiguration configuration)
        {
            throw new NotImplementedException();
        }
    }
}
