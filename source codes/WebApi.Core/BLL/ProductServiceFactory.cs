using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Threading;

namespace WebApi.BLL
{
    public class ProductServiceFactory
    {
        public IProductService GetProductService()
        {
            IPrincipal principal = Thread.CurrentPrincipal;

            if (principal == null)
            {
                throw new InvalidOperationException();
            }

            var categoryDao = DaoFactory.GetCategoryDao();
            var productDao = DaoFactory.GetProductDao();

            if (principal.IsInRole("Seven11"))
            {
                return new Seven11ProductService(categoryDao, productDao);
            }

            if (principal.IsInRole("FamilyMart"))
            {
                return new FamilyMartProductService(categoryDao, productDao);
            }

            //throw new NotSupportedException();
            return new Seven11ProductService(categoryDao, productDao);
        }
    }
}
