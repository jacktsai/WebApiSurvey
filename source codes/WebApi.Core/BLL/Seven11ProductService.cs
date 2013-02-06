using System.Collections.Generic;
using System.Linq;
using WebApi.BLL.Entities;
using WebApi.DAL;

namespace WebApi.BLL
{
    /// <summary>
    /// A concrete class of <see cref="IProductService"/> interface.
    /// The class manages all product for 7-11.
    /// </summary>
    public class Seven11ProductService : IProductService
    {
        private ICategoryDao categoryDao;
        private IProductDao productDao;

        public Seven11ProductService(ICategoryDao categoryDao, IProductDao productDao)
        {
            this.categoryDao = categoryDao;
            this.productDao = productDao;
        }

        public IEnumerable<Category> GetCategories()
        {
            var args = new CategorySelectArguments
            {
                Store = StoreEnum.Seven11,
            };

            return categoryDao.Select(args);
        }

        public IEnumerable<Product> GetProducts(int categoryId)
        {
            var args = new ProductSelectArguments
            {
                CategoryId = categoryId,
            };

            return productDao.Select(args);
        }
    }
}