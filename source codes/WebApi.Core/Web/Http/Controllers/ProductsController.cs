using System.Collections.Generic;
using System.Web.Http;
using WebApi.BLL;
using WebApi.BLL.Entities;
using System.Web.Http.Description;
using System.Web.Http.Tracing;
using System;
using System.Net.Http;
using System.Net;
using System.Linq;
using System.Web.Http.Controllers;
using System.Threading;
using System.Web.Http.Dispatcher;
using System.Web.Http.Filters;

namespace WebApi.Web.Http.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductService productService;

        public ProductsController()
        {
            this.productService = new ProductServiceFactory().GetProductService();
        }

        [Queryable]
        public IEnumerable<Category> GetCategories()
        {
            var writer = base.Configuration.Services.GetTraceWriter();
            if (writer != null)
            {
                writer.Trace(base.Request, "ProductsController", TraceLevel.Info, r => r.Message = "Get categories");
            }

            return this.productService.GetCategories();
        }

        [MyAuthorize]
        public IEnumerable<Product> GetProducts(int categoryId)
        {
            return this.productService.GetProducts(categoryId);
        }

        [Queryable]
        public IEnumerable<Product> GetProducts()
        {
            return this.productService.GetProducts(null);
        }

        [HttpPost]
        [AcceptVerbs("POST", "PUT")]
        public Product InsertProduct(Product product)
        {
            return product;
        }

        public string GetStoreName(string store)
        {
            StoreEnum value;
            if (Enum.TryParse<StoreEnum>(store, out value))
            {
                return value.ToString();
            }

            return "Unknown";
        }
    }
}