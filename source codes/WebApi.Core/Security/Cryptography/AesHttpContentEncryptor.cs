using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;
using System.Web.Http.Tracing;

namespace WebApi.Security.Cryptography
{
    public class AesHttpContentEncryptor : IHttpContentEncryptor
    {
        private readonly SymmetricAlgorithm algorithm;

        public AesHttpContentEncryptor()
        {
            algorithm = new AesManaged();
        }

        HttpContent IHttpContentEncryptor.Encrypt(HttpContent originContent)
        {
            if (originContent == null)
            {
                throw new ArgumentNullException("originContent");
            }

            var originStream = originContent.ReadAsStreamAsync().Result;
            if (originStream.Length == 0)
            {
                return originContent;
            }

            using (var encryptor = algorithm.CreateEncryptor())
            {
                using (var encryptedData = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(encryptedData, encryptor, CryptoStreamMode.Write))
                    {
                        originStream.CopyTo(cryptoStream);
                    }

                    var encryptedBytes = encryptedData.ToArray();
                    var encodedString = Convert.ToBase64String(encryptedBytes);
                    Trace.WriteLine(string.Format("[Encrypt]EncodedString: {0}", encodedString));
                    var encodedContent = new StringContent(encodedString,Encoding.UTF8);
                    encodedContent.Headers.ContentType = originContent.Headers.ContentType;
                    return encodedContent;
                }
            }
        }

        HttpContent IHttpContentEncryptor.Decrypt(HttpContent encryptedContent)
        {
            if (encryptedContent == null)
            {
                throw new ArgumentNullException("encryptedContent");
            }

            //var encodedString = encryptedContent.ReadAsStringAsync().Result;

            var encodedString = string.Empty;
            var encryptedStream = encryptedContent.ReadAsStreamAsync().Result;
            using (var streamReader = new StreamReader(encryptedStream, Encoding.UTF8))
            {
                encodedString = streamReader.ReadToEnd();
            }

            Trace.WriteLine(string.Format("[Decrypt]EncodedString: {0}", encodedString));
            if (string.IsNullOrEmpty(encodedString))
            {
                return encryptedContent;
            }

            using (var decryptor = algorithm.CreateDecryptor())
            {
                var encryptedBytes = Convert.FromBase64String(encodedString);

                using (var encryptedData = new MemoryStream(encryptedBytes))
                {
                    using (var cryptoStream = new CryptoStream(encryptedData, decryptor, CryptoStreamMode.Read))
                    {
                        var originStream = new MemoryStream((int)encryptedData.Length);
                        cryptoStream.CopyTo(originStream);
                        originStream.Position = 0;

                        var decodedContent = new StreamContent(originStream);
                        decodedContent.Headers.ContentType = encryptedContent.Headers.ContentType;
                        return decodedContent;
                    }
                }
            }
        }

        void IDisposable.Dispose()
        {
            algorithm.Dispose();
        }
    }
}
