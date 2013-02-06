using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http.Headers;
using System.Threading;
using System.Web;
using System.Security.Principal;

namespace WebApi.BLL.Authentication
{
    internal class SimpleAuthentication : IAuthentication
    {
        bool IAuthentication.Authenticate(string userId, string password)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("userId");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            IPrincipal principal = null;
            if (userId == "jacktsai" && password == "1234")
            {
                var identity = new GenericIdentity("Jack Tsai", "NTLM");
                principal = new GenericPrincipal(identity, new[] { "Yahoo!" });
            }

            if (principal != null)
            {
                SetupPrincipal(principal);
                return true;
            }

            return false;
        }

        bool IAuthentication.Authenticate(AuthenticationHeaderValue value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            var identity = new GenericIdentity(value.Parameter, "NTLM");
            var principal = new GenericPrincipal(identity, new[] { value.Scheme });
            SetupPrincipal(principal);

            return true;
        }

        void SetupPrincipal(IPrincipal principal)
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
            Thread.CurrentPrincipal = principal;
        }
    }
}
