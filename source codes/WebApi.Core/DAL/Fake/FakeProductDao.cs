using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.BLL.Entities;

namespace WebApi.DAL.Fake
{
    public sealed class FakeProductDao : IProductDao
    {
        public IEnumerable<Product> Select(ProductSelectArguments args)
        {
            var result = from c in Repository.Categories
                         from p in Repository.Products
                         from x in Repository.Prices
                         where c.category_id == x.category_id && p.product_id == x.product_id
                         select new
                         {
                             category = c,
                             product = p,
                             price = x,
                         };

            if (args != null)
            {
                if (args.CategoryId != null)
                {
                    result = result.Where(o => o.category.category_id == args.CategoryId.Value);
                }
            }

            return from o in result
                   select new Product
                   {
                       Category = new Category
                       {
                           Id = o.category.category_id,
                           Store = (StoreEnum)o.category.category_store,
                           Name = o.category.category_name,
                       },
                       Id = o.product.product_id,
                       Name = o.product.product_name,
                       Cost = o.price.product_cost,
                       Price = o.price.product_price,
                   };
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Insert(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            Delete(entity);
            Insert(entity);
        }
    }
}