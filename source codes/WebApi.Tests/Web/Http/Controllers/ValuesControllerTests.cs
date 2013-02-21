using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Web.Http.SelfHost;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Threading;
using WebApi.Net.Http;

namespace WebApi.Web.Http.Controllers
{
    [TestClass]
    public class ValuesControllerTests
    {
        private static readonly string BaseAddr = "http://localhost:8080";

        //private static HttpSelfHostServer server;

        //[AssemblyInitialize]
        //public static void ClassInitialize(TestContext context)
        //{
        //    var config = new HttpSelfHostConfiguration(BaseAddr);
        //    MyConfig.Config(config);
        //    server = new HttpSelfHostServer(config);
        //    server.OpenAsync().Wait();
        //}

        //[AssemblyCleanup]
        //public static void ClassCleanup()
        //{
        //    server.CloseAsync().Wait();
        //    server.Dispose();
        //}

        private TestContext context;
        private HttpClient client;

        [TestInitialize]
        public void TestInitialize()
        {
            //var handler = MyConfig.GetService<ClientSideSecurityHandler>();
            //handler.InnerHandler = new HttpClientHandler();
            var handler = new HttpClientHandler();

            client = new HttpClient(handler);
            client.BaseAddress = new Uri(BaseAddr);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            client.Dispose();
        }

        public TestContext TestContext { set { this.context = value; } }

        [TestMethod]
        public void GetTest()
        {
            var response = client.GetAsync("api/values/get").Result;
            context.WriteLine("StatusCode: {0}", response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);

            var actual = response.Content.ReadAsAsync<IEnumerable<string>>().Result.ToArray();
            Assert.AreEqual(2, actual.Length);
            Assert.AreEqual("value1", actual[0]);
            Assert.AreEqual("value2", actual[1]);
        }

        [TestMethod]
        public void GetTest_with_id()
        {
            var expected = string.Format("id {0}'s value", 2);
            var response = client.GetAsync("api/values/get?id=2").Result;
            context.WriteLine("StatusCode: {0}", response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);

            var actual = response.Content.ReadAsAsync<string>().Result;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PostTest()
        {
            var expected = "this is a string !!";
            var response = client.PostAsJsonAsync("api/values/post", expected).Result;
            context.WriteLine("StatusCode: {0}", response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);

            var actual = response.Content.ReadAsAsync<string>().Result;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PutTest()
        {
            var expected = string.Format("id={0}, value={1}.", 3, "this is a string");
            var response = client.PutAsJsonAsync("api/values/put?id=3", "this is a string").Result;
            context.WriteLine("StatusCode: {0}", response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);

            var actual = response.Content.ReadAsAsync<string>().Result;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var expected = 5;
            var response = client.DeleteAsync("api/values/delete?id=5").Result;
            context.WriteLine("StatusCode: {0}", response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);

            var actual = response.Content.ReadAsAsync<int>().Result;
            Assert.AreEqual(expected, actual);
        }

        //[TestMethod]
        public void GetWithContentTest()
        {
            var expected = new JObject();
            expected["str"] = "this is a string !!";
            expected["int"] = 12345;
            expected["float"] = 123.45;

            var request = new HttpRequestMessage(HttpMethod.Get, "api/values/GetWithContent/");
            //如果附上 content 會跑不完。
            //request.Content = new ObjectContent<JObject>(expected, new JsonMediaTypeFormatter());
            var response = client.SendAsync(request).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);

            //var actual = response.Content.ReadAsAsync<IEnumerable<string>>().Result.ToArray();
            //Assert.AreEqual(2, actual.Length);
            //Assert.AreEqual("value1", actual[0]);
            //Assert.AreEqual("value2", actual[1]);
        }

        [TestMethod]
        public void PostAnyObjectTest()
        {
            var expected = new JObject();
            expected["str"] = "this is a string !!";
            expected["int"] = 12345;
            expected["float"] = 123.45;
            var response = client.PostAsJsonAsync("api/values/PostAnyObject", expected).Result;
            context.WriteLine("StatusCode: {0}", response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);

            var actual = response.Content.ReadAsAsync<JObject>().Result;
            Assert.AreEqual(expected["str"], actual["str"]);
            Assert.AreEqual(expected["int"], actual["int"]);
            Assert.AreEqual(expected["float"], actual["float"]);
        }
    }
}
