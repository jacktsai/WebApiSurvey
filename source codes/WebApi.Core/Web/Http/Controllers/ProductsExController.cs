using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using WebApi.BLL;
using WebApi.BLL.Entities;

namespace WebApi.Web.Http.Controllers
{
    /// <summary>
    /// 仿 ProductController，唯一不同的是 IProductService 實體將於建構時傳入。
    /// </summary>
    public class ProductsExController : ApiController
    {
        private readonly IProductService productService;

        public ProductsExController(IProductService productService)
        {
            this.productService = productService;
        }

        public IEnumerable<Category> GetCategories()
        {
            return this.productService.GetCategories();
        }

        public IEnumerable<Product> GetProducts(int? categoryId)
        {
            return this.productService.GetProducts(categoryId);
        }
    }
}
