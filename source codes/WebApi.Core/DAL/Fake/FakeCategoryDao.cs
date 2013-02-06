using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.BLL.Entities;

namespace WebApi.DAL.Fake
{
    public sealed class FakeCategoryDao : ICategoryDao
    {
        public IEnumerable<Category> Select(CategorySelectArguments args)
        {
            var result = Repository.Categories.AsEnumerable();

            if (args != null)
            {
                if (args.Id != null)
                {
                    result = result.Where(o => o.category_id == args.Id.Value);
                }

                if (args.Store != null)
                {
                    result = result.Where(o => o.category_store == (int)args.Store.Value);
                }

                if (args.ParentId != null)
                {
                    result = result.Where(o => o.category_parent == args.ParentId.Value);
                }
            }

            return from o in result
                   select new Category
                   {
                       Id = o.category_id,
                       Store = (StoreEnum)o.category_store,
                       Name = o.category_name,
                       Description = o.category_desc,
                   };
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public void Insert(Category entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Category entity)
        {
            Delete(entity);
            Insert(entity);
        }
    }
}