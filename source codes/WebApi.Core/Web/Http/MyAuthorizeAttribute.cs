using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Filters;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Threading;
using System.Net.Http.Headers;
using System.Net;
using WebApi.Web.Http.Controllers;
using System.Web.Http;

namespace WebApi.Web.Http
{
    internal class MyAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 注意：如果 controller 或 action 已標記為 [AllowAnonymous]，則此方法不會被執行。
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            // 原 IsAuthorized 會判斷 Thread.CurrentPrincipal 是否在 Users 及 Roles 裡。
            bool authorized = base.IsAuthorized(actionContext);

            // 多一層授權檢查
            if (authorized)
            {
                authorized = false;

                var query = actionContext.Request.GetQueryNameValuePairs();
                foreach (var pair in query)
                {
                    if (pair.Key == "pass")
                    {
                        authorized = pair.Value == "true";
                        break;
                    }
                }
            }

            return authorized;
        }
    }
}
