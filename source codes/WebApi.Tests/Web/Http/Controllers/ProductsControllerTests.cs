using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using WebApi.BLL;
using WebApi.BLL.Entities;
using WebApi.DAL;
using System.Web.Http.SelfHost;
using System.Web.Http;
using System.Net.Http;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;
using WebApi.Net.Http;

namespace WebApi.Web.Http.Controllers
{
    [TestClass]
    public class ProductsControllerTests
    {
        private static readonly string BaseAddr = "http://localhost:8080";

        private TestContext context;
        private HttpClient client;

        [TestInitialize]
        public void TestInitialize()
        {
            var securityHandler = MyConfig.GetService<ClientSideSecurityHandler>();
            securityHandler.InnerHandler = new HttpClientHandler();

            client = new HttpClient(securityHandler);
            client.BaseAddress = new Uri(BaseAddr);

            if (!context.TestName.Contains("Unauthorized"))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Seven11", "Jack Tsai");
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            client.Dispose();
        }

        public TestContext TestContext { set { this.context = value; } }

        [TestMethod]
        public void GetCategoriesTest()
        {
            var response = client.GetAsync("api/Products/GetCategories").Result;
            context.WriteLine("StatusCode: {0}", response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);

            var result = response.Content.ReadAsAsync<IEnumerable<Category>>().Result;
            foreach (var item in result)
            {
                context.WriteLine("Id:{0}, Store:{1}, Name:{2}, Desc:{3}.", item.Id, item.Store, item.Name, item.Description);
            }
        }

        [TestMethod]
        public void GetProductsTest_1_Authorized()
        {
            var categoryId = 1;
            var uri = string.Format("api/Products/GetProducts?categoryId={0}", categoryId);
            var response = client.GetAsync(uri).Result;
            context.WriteLine("StatusCode: {0}", response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);

            var result = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            foreach (var item in result)
            {
                var c = item.Category;
                context.WriteLine("Category Id:{0}, Store:{1}, Name:{2}, Desc:{3}.", c.Id, c.Store, c.Name, c.Description);
                context.WriteLine("Product Id:{0}, Name:{1}, Cost:{2}, Price:{3}.", item.Id, item.Name, item.Cost, item.Price);
                Assert.AreEqual(categoryId, c.Id);
            }
        }

        [TestMethod]
        public void GetProductsTest_1_Unauthorized()
        {
            var categoryId = 1;
            var uri = string.Format("api/Products/GetProducts?categoryId={0}", categoryId);
            var response = client.GetAsync(uri).Result;
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        public void GetProductsTest_NotFound()
        {
            var response = client.GetAsync("api/Products/GetProducts/hello").Result;
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void InsertProductTest_ValidModel()
        {
            var product = new { Id = 1, Name = "Jack Tsai", Price = 123, Weight = 1 };
            var request = new HttpRequestMessage(HttpMethod.Post, "api/products/InsertProduct");
            request.Content = new ObjectContent(product.GetType(), product, new JsonMediaTypeFormatter());
            var response = client.SendAsync(request).Result;
            context.WriteLine("StatusCode: {0}", response.StatusCode);
            Assert.IsTrue(response.IsSuccessStatusCode);

            var responseProduct = response.Content.ReadAsAsync<Product>().Result;
            Assert.AreEqual(product.Id, responseProduct.Id);
            Assert.AreEqual(product.Name, responseProduct.Name);
            Assert.AreEqual(product.Price, responseProduct.Price);
        }

        [TestMethod]
        public void InsertProductTest_InvalidModel()
        {
            var product = new { Weight = 101 };

            var request = new HttpRequestMessage(HttpMethod.Post, "api/products/InsertProduct");
            request.Content = new ObjectContent(product.GetType(), product, new JsonMediaTypeFormatter());
            var response = client.SendAsync(request).Result;
            context.WriteLine("StatusCode: {0}", response.StatusCode);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

            var content = response.Content.ReadAsStringAsync().Result;
            context.WriteLine("Response: {0}", content);
            Assert.IsTrue(content.Contains("The Id field is required."));
            Assert.IsTrue(content.Contains("The Name field is required."));
            Assert.IsTrue(content.Contains("The field Weight must be between 1 and 100."));
        }

        [TestMethod]
        public void InsertProductTest_FieldOrderValidation_SourceIsJson()
        {
            // does not care about sequence of fields.
            var content = new StringContent("{\"Weight\":1,\"SupplierId\":2360,\"Id\":123,\"Name\":\"some product\",\"Price\":1000.0}");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/products/InsertProduct")
            {
                Content = content
            };
            var response = client.SendAsync(request).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);

            var responseProduct = response.Content.ReadAsAsync<Product>().Result;
            Assert.AreEqual(123, responseProduct.Id);
            Assert.AreEqual("some product", responseProduct.Name);
            Assert.AreEqual(1000, responseProduct.Price);
            Assert.AreEqual(1, responseProduct.Weight);
            Assert.AreEqual(2360, responseProduct.SupplierId);
        }

        [TestMethod]
        public void InsertProductTest_FieldOrderValidation_SourceIsXml_ValidSequence()
        {
            // does care about sequence of fields.
            var content = new StringContent(@"
<Product xmlns='http://schemas.datacontract.org/2004/07/WebApi.BLL.Entities'>
    <Id>123</Id>
    <Name>some product</Name>
    <Price>1000</Price>
    <SupplierId>2360</SupplierId>
    <Weight>1</Weight>
</Product>");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/products/InsertProduct")
            {
                Content = content
            };
            var response = client.SendAsync(request).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);

            var responseProduct = response.Content.ReadAsAsync<Product>().Result;
            Assert.AreEqual(123, responseProduct.Id);
            Assert.AreEqual("some product", responseProduct.Name);
            Assert.AreEqual(1000, responseProduct.Price);
            Assert.AreEqual(1, responseProduct.Weight);
            Assert.AreEqual(2360, responseProduct.SupplierId);
        }

        [TestMethod]
        public void InsertProductTest_FieldOrderValidation_SourceIsXml_InvalidSequence()
        {
            // does care about sequence of fields.
            var content = new StringContent(@"
<Product xmlns='http://schemas.datacontract.org/2004/07/WebApi.BLL.Entities'>
    <Weight>1</Weight>
    <SupplierId>2360</SupplierId>
    <Id>123</Id>
    <Name>some product</Name>
    <Price>1000</Price>
</Product>");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/products/InsertProduct")
            {
                Content = content
            };
            var response = client.SendAsync(request).Result;

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorList = response.Content.ReadAsAsync<Dictionary<string, IEnumerable<string>>>().Result;
                foreach (var pair in errorList)
                {
                    context.WriteLine("[{0}]", pair.Key);
                    foreach (var msg in pair.Value)
                    {
                        context.WriteLine("\t{0}", msg);
                    }
                }
            }

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
