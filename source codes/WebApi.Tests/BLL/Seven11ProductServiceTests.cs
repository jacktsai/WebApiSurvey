using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using WebApi.BLL;
using WebApi.BLL.Entities;
using WebApi.DAL;

namespace WebApi.BLL
{
    [TestClass()]
    public class Seven11ProductServiceTests
    {
        [TestMethod()]
        public void GetCategoriesTest()
        {
            // Arrange
            var categoryDao = MockRepository.GenerateMock<ICategoryDao>();
            var productDao = MockRepository.GenerateMock<IProductDao>();
            var expected = new Category[]
            {
                new Category { Id = 1, Store = StoreEnum.Seven11 },
                new Category { Id = 3, Store = StoreEnum.Seven11 },
            };
            categoryDao.Stub(o => o.Select(null))
                .IgnoreArguments()
                .Return(expected);

            // Action
            Seven11ProductService target = new Seven11ProductService(categoryDao, productDao);
            var actual = target.GetCategories().ToArray();
            
            // Assertion
            categoryDao.AssertWasCalled(o => o.Select(Arg<CategorySelectArguments>.Matches(a => a.Store == StoreEnum.Seven11)));
            Assert.AreEqual(expected.Length, actual.Length);
            Assert.AreEqual(expected[0].Id, actual[0].Id);
            Assert.AreEqual(expected[1].Id, actual[1].Id);
        }

        [TestMethod]
        public void GetProductsTest()
        {
            // Arrange
            var categoryDao = MockRepository.GenerateMock<ICategoryDao>();
            var productDao = MockRepository.GenerateMock<IProductDao>();
            var categoryId = 1;
            var expected = new Product[]
            {
                new Product { Id = 1 },
                new Product { Id = 3 },
            };
            productDao.Stub(o => o.Select(null))
                .IgnoreArguments()
                .Return(expected);

            // Action
            Seven11ProductService target = new Seven11ProductService(categoryDao, productDao);
            var actual = target.GetProducts(categoryId).ToArray();

            // Assertion
            productDao.AssertWasCalled(o => o.Select(Arg<ProductSelectArguments>.Matches(a => a.CategoryId == categoryId)));
            Assert.AreEqual(expected.Length, actual.Length);
            Assert.AreEqual(expected[0].Id, actual[0].Id);
            Assert.AreEqual(expected[1].Id, actual[1].Id);
        }
    }
}