using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Net.Http.Headers;

namespace WebApi.BLL.Authentication
{
    /// <summary>
    /// The interface of authentication.
    /// </summary>
    public interface IAuthentication
    {
        /// <summary>
        /// Authenticates the specified user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="password">The password.</param>
        bool Authenticate(string userId, string password);

        /// <summary>
        /// Authenticates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        bool Authenticate(AuthenticationHeaderValue value);
    }
}
