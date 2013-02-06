using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Web.Http.Tracing;
using WebApi.Web.Http.Description;
using System.Web.Http.Description;

namespace WebApi.Web.Http.Controllers
{
    [AllowAnonymous]
    public class ValuesController : ApiController
    {
        public ValuesController()
        {
        }

        // GET api/values/get
        [ActionDoc("Get all values.")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/get?id={id}
        [ActionDoc("Get value by id.")]
        public string Get([ParameterDoc("an ID.")]int id)
        {
            return string.Format("id {0}'s value", id);
        }

        // GET api/values/get?id={id}&name={name}
        [ActionDoc("Get value by id and name.")]
        public string Get(int id, string name)
        {
            return string.Format("id {0}'s value is {1}", id, name);
        }

        // POST api/values
        public string Post([FromBody]string value)
        {
            var writer = base.Configuration.Services.GetTraceWriter();
            if (writer != null)
            {
                writer.Trace(base.Request, "ValuesController", TraceLevel.Info, r => r.Message = "I have received a post message!");
                writer.Trace(base.Request, "ValuesController", TraceLevel.Info, r => r.Message = string.Format("Post with '{0}'.", value));
            }

            return value;
        }

        // PUT api/values/5
        public string Put(int id, [FromBody]string value)
        {
            return string.Format("id={0}, value={1}.", id, value);
        }

        // DELETE api/values/5
        public int Delete(int id)
        {
            return id;
        }

        public JObject GetWithContent(JObject obj)
        {
            return obj;
        }

        [MyAuthorize]
        public JObject PostAnyObject([FromBody]JObject obj)
        {
            return obj;
        }

        [HttpPost]
        [MyAuthorize]
        [ApiExplorerSettings(IgnoreApi = true)]
        public JObject SomeMethod([FromBody]JObject obj)
        {
            return obj;
        }
    }
}