using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.BLL.Authentication
{
    public class WindowsAuthentication : IAuthentication
    {
        bool IAuthentication.Authenticate(string userId, string password)
        {
            throw new NotImplementedException();
        }

        bool IAuthentication.Authenticate(System.Net.Http.Headers.AuthenticationHeaderValue value)
        {
            throw new NotImplementedException();
        }
    }
}
