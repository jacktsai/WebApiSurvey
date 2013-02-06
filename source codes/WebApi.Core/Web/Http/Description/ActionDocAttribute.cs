using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.Web.Http.Description
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ActionDocAttribute : Attribute
    {
        public ActionDocAttribute(string documentation)
        {
            Documentation = documentation;
        }

        public string Documentation { get; set; }
    }
}
