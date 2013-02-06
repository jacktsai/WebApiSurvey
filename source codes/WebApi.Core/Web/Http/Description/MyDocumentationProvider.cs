using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Description;
using System.Web.Http.Controllers;

namespace WebApi.Web.Http.Description
{
    class MyDocumentationProvider : IDocumentationProvider
    {
        string IDocumentationProvider.GetDocumentation(HttpActionDescriptor actionDescriptor)
        {
            var actionDoc = actionDescriptor.GetCustomAttributes<ActionDocAttribute>().FirstOrDefault();
            if (actionDoc != null)
            {
                return actionDoc.Documentation;
            }

            return string.Format("Documentation for '{0}'.", actionDescriptor.ActionName);
        }

        string IDocumentationProvider.GetDocumentation(HttpParameterDescriptor parameterDescriptor)
        {
            var actionDoc = parameterDescriptor.GetCustomAttributes<ParameterDocAttribute>().FirstOrDefault();
            if (actionDoc != null)
            {
                return actionDoc.Documentation;
            }

            return string.Format("Documentation for '{0}'.", parameterDescriptor.ParameterName);
        }
    }
}
