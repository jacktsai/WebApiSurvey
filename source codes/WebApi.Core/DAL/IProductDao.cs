using WebApi.BLL.Entities;

namespace WebApi.DAL
{
    public interface IProductDao : IDao<Product, ProductSelectArguments>
    {
    }
}