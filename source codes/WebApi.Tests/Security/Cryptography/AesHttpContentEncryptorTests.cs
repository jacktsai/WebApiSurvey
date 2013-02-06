using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace WebApi.Security.Cryptography
{
    [TestClass]
    public class AesHttpContentEncryptorTests : HttpContentEncryptorTests
    {
        protected override IHttpContentEncryptor CreateEncryptor()
        {
            return new AesHttpContentEncryptor(new DefaultKeyProvider());
        }
    }
}
