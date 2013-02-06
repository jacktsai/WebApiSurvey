using System.Collections.Generic;
using WebApi.BLL.Entities;

namespace WebApi.BLL
{
    public interface IProductService
    {
        IEnumerable<Category> GetCategories();

        IEnumerable<Product> GetProducts(int categoryId);
    }
}