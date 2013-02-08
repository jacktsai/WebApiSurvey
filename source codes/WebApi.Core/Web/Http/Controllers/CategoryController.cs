using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.OData;
using WebApi.BLL.Entities;
using WebApi.BLL;
using System.Web.Http;

namespace WebApi.Web.Http.Controllers
{
    public class CategoryController : EntitySetController<Category, int>
    {
        private readonly IProductService productService;

        public CategoryController()
        {
            this.productService = new ProductServiceFactory().GetProductService();
        }

        [Queryable]
        public override IQueryable<Category> Get()
        {
            return this.productService.GetCategories().AsQueryable();
        }

        protected override Category GetEntityByKey(int key)
        {
            return this.productService.GetCategories().FirstOrDefault(p => p.Id == key);
        }

        protected override Category PatchEntity(int key, Delta<Category> patch)
        {
            return GetEntityByKey(key);
        }
    }
}
