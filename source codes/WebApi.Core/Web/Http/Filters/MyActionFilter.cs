using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using System.Net.Http;
using System.Net;
using System.Diagnostics;

namespace WebApi.Web.Http.Filters
{
    internal class MyActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Trace.WriteLine(string.Format("{0}.{1}, ModelState={2}", actionContext.ControllerContext.ControllerDescriptor.ControllerName, actionContext.ActionDescriptor.ActionName, actionContext.ModelState.IsValid));

            if (actionContext.ModelState.IsValid)
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                var errorList = new Dictionary<string, IEnumerable<string>>();
                foreach (var pair in actionContext.ModelState)
                {
                    var errors = pair.Value.Errors;
                    if (errors.Count > 0)
                    {
                        errorList[pair.Key] = errors.Select(e =>
                        {
                            if (string.IsNullOrEmpty(e.ErrorMessage))
                            {
                                return e.Exception.Message;
                            }
                            return e.ErrorMessage;
                        });
                    }
                }
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, errorList);
            }
        }
    }
}
