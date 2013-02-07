using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Tracing;
using System.Reflection;
using System.IO;
using System.Web;
using System.Net.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Diagnostics;

namespace WebApi.IO
{
    internal class FileRepository
    {
        readonly IDictionary<Guid, string> fileMap = new Dictionary<Guid, string>();

        protected string GetDirectoryPath(string directoryName)
        {
            var codeBase = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var codeDir = Path.GetDirectoryName(codeBase);
            var path = Path.Combine(codeDir, directoryName);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public void LogRequest(HttpRequestMessage request)
        {
            var dirPath = GetDirectoryPath("Requests");
            var fileName = DateTime.Now.ToString("yyyyMMdd_HHmmssffffff'.log'");
            var filePath = Path.Combine(dirPath, fileName);

            fileMap[request.GetCorrelationId()] = filePath;

            StringBuilder msg = new StringBuilder(1024);
            msg.AppendLine("[Request]");
            msg.AppendLine(string.Format("{0} {1} HTTP/{2}", request.Method, request.RequestUri.PathAndQuery, request.Version));
            foreach (var header in request.Headers)
            {
                msg.AppendFormat("{0}: ", header.Key);
                foreach (var value in header.Value)
                {
                    msg.AppendFormat("{0};", value);
                }
                msg.AppendLine();
            }

            var content = request.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(content))
            {
                msg.AppendLine();
                msg.AppendLine(content);
            }

            Trace.WriteLine(msg);

            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine(msg.ToString());
            }
        }

        public void LogResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            StringBuilder msg = new StringBuilder(1024);
            msg.AppendLine("[Response]");
            foreach (var header in response.Headers)
            {
                msg.AppendFormat("{0}: ", header.Key);
                foreach (var value in header.Value)
                {
                    msg.AppendFormat("{0};", value);
                }
                msg.AppendLine();
            }

            var content = response.Content.ReadAsStringAsync().Result;
            if (!string.IsNullOrEmpty(content))
            {
                msg.AppendLine();
                msg.AppendLine(content);
            }

            Trace.WriteLine(msg);

            var filePath = fileMap[request.GetCorrelationId()];
            using (var writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(msg.ToString());
            }
        }

        public void LogTraceRecord(TraceRecord record)
        {
            string fileName;
            if (record.Request == null)
            {
                fileName = "All";
            }
            else
            {
                fileName = record.RequestId.ToString();
            }

            var dirPath = GetDirectoryPath("Traces");
            var filePath = Path.Combine(dirPath, fileName + ".txt");

            using (var writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("{0:HH:mm:ss.fff} {1} {2} {3} {4} {5} {6}", record.Timestamp, record.Level, record.Category, record.Operator, record.Kind, record.Operation, record.Message);
                writer.Flush();
            }
        }
    }
}
