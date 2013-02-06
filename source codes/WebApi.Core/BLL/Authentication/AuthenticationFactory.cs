using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.BLL.Authentication
{
    public class AuthenticationFactory
    {
        public IAuthentication GetAuthentication()
        {
            return new SimpleAuthentication();
        }
    }
}
