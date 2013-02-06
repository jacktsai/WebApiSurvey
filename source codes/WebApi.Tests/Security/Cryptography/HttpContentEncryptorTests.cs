using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.IO;
using System.Net.Http.Formatting;

namespace WebApi.Security.Cryptography
{
    [TestClass]
    public abstract class HttpContentEncryptorTests
    {
        public TestContext TestContext { get; set; }

        private IHttpContentEncryptor target;

        [TestInitialize]
        public void TestInitialize()
        {
            target = CreateEncryptor();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            target.Dispose();
        }

        protected abstract IHttpContentEncryptor CreateEncryptor();

        [TestMethod]
        public void EncryptTest_Stream()
        {
            var expected = new MemoryStream(new byte[] { 0x01, 0x12, 0x23, 0x34, 0x45, 0x56, 0x67, 0x78, 0x89, 0x90 });
            var originContent = new StreamContent(expected);
            var encryptedContent = target.Encrypt(originContent);
            var decryptedContent = target.Decrypt(encryptedContent);
            var actual = decryptedContent.ReadAsStreamAsync().Result;

            expected.Position = 0;
            Assert.AreEqual(expected.Length, actual.Length, "Assert Stream.Length");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected.ReadByte(), actual.ReadByte());
            }
        }

        [TestMethod]
        public void EncryptTest_String()
        {
            var expected = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            var originContent = new StringContent(expected);
            var encryptedContent = target.Encrypt(originContent);
            var decryptedContent = target.Decrypt(encryptedContent);
            var actual = decryptedContent.ReadAsStringAsync().Result;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncryptTest_ByteArray()
        {
            var expected = new byte[] { 0x01, 0x12, 0x23, 0x34, 0x45, 0x56, 0x67, 0x78, 0x89, 0x90 };
            var originContent = new ByteArrayContent(expected);
            var encryptedContent = target.Encrypt(originContent);
            var decryptedContent = target.Decrypt(encryptedContent);
            var actual = decryptedContent.ReadAsByteArrayAsync().Result;

            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public void EncryptTest_StringObject()
        {
            var expected = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            var originContent = new ObjectContent<string>(expected, new JsonMediaTypeFormatter());
            var encryptedContent = target.Encrypt(originContent);
            var decryptedContent = target.Decrypt(encryptedContent);
            var actual = decryptedContent.ReadAsAsync<string>().Result;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EncryptTest_Int32Object()
        {
            var expected = Int32.MaxValue;
            var originContent = new ObjectContent<Int32>(expected, new JsonMediaTypeFormatter());
            var encryptedContent = target.Encrypt(originContent);
            var decryptedContent = target.Decrypt(encryptedContent);
            var actual = decryptedContent.ReadAsAsync<Int32>().Result;

            Assert.AreEqual(expected, actual);
        }
    }
}
