using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using WebApi.BLL;
using System.Net;
using WebApi.Security.Cryptography;

namespace WebApi.Net.Http
{
    public class ServerSideSecurityHandler : MessageProcessingHandler
    {
        IHttpContentEncryptor encryptor;

        public ServerSideSecurityHandler(IHttpContentEncryptor encryptor)
        {
            this.encryptor = encryptor;
        }

        protected override HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content != null)
            {
                request.Content = encryptor.Decrypt(request.Content);
            }
            return request;
        }

        protected override HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
            {
                response.Content = encryptor.Encrypt(response.Content);
            }
            return response;
        }
    }
}
