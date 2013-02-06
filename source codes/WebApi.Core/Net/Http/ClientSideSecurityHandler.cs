using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using WebApi.BLL;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using WebApi.Security.Cryptography;

namespace WebApi.Net.Http
{
    public class ClientSideSecurityHandler : MessageProcessingHandler
    {
        IHttpContentEncryptor encryptor;

        public ClientSideSecurityHandler(IHttpContentEncryptor encryptor)
        {
            this.encryptor = encryptor;
        }

        protected override HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content != null)
            {
                request.Content = encryptor.Encrypt(request.Content);
            }
            return request;
        }

        protected override HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
            {
                response.Content = encryptor.Decrypt(response.Content);
            }
            return response;
        }
    }
}
