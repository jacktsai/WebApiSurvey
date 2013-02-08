using System;
using System.Collections.Generic;
using WebApi.BLL.Entities;
using WebApi.DAL;

namespace WebApi.BLL
{
    /// <summary>
    /// A concrete class of <see cref="IProductService"/> interface.
    /// The class manages all product for FamilyMart.
    /// </summary>
    public class FamilyMartProductService : IProductService
    {
        private ICategoryDao categoryDao;
        private IProductDao productDao;

        public FamilyMartProductService(ICategoryDao categoryDao, IProductDao productDao)
        {
            this.categoryDao = categoryDao;
            this.productDao = productDao;
        }

        public IEnumerable<Category> GetCategories()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts(int? categoryId)
        {
            throw new NotImplementedException();
        }
    }
}