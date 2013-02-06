using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.Web.Http.Description
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ParameterDocAttribute : Attribute
    {
        public ParameterDocAttribute(string documentation)
        {
            Documentation = documentation;
        }

        public string Documentation { get; set; }
    }
}
