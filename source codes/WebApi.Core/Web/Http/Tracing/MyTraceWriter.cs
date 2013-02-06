using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Tracing;
using System.Net.Http;
using System.IO;
using System.Web;
using System.Reflection;
using WebApi.IO;

namespace WebApi.Web.Http.Tracing
{
    internal class MyTraceWriter : ITraceWriter
    {
        private readonly FileRepository fileRep = new FileRepository();

        void ITraceWriter.Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            var record = new TraceRecord(request, category, level);
            traceAction(record);
            fileRep.LogTraceRecord(record);
        }
    }
}
