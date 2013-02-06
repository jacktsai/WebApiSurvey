using WebApi.DAL;
using WebApi.DAL.Fake;

namespace WebApi.BLL
{
    public static class DaoFactory
    {
        private static ICategoryDao _categoryDao = null;

        public static ICategoryDao GetCategoryDao()
        {
            if (_categoryDao == null)
            {
                _categoryDao = new FakeCategoryDao();
            }
            return _categoryDao;
        }

        private static IProductDao _productDao = null;

        public static IProductDao GetProductDao()
        {
            if (_productDao == null)
            {
                _productDao = new FakeProductDao();
            }
            return _productDao;
        }
    }
}