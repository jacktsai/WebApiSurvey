using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi.BLL.Entities;

namespace WebApi.DAL.Fake
{
    /// <summary>
    /// 模擬資料庫儲存空間。
    /// </summary>
    internal static class Repository
    {
        /// <summary>
        /// 模擬目錄實體資料表。
        /// </summary>
        internal class category
        {
            /// <summary>
            /// 商店編號。
            /// </summary>
            public int category_store { get; set; }

            /// <summary>
            /// 目錄編號。
            /// </summary>
            public int category_id { get; set; }

            /// <summary>
            /// 目錄名稱。
            /// </summary>
            public string category_name { get; set; }

            /// <summary>
            /// 目錄描述。
            /// </summary>
            public string category_desc { get; set; }

            /// <summary>
            /// 父目錄編號。
            /// </summary>
            public int? category_parent { get; set; }
        }

        /// <summary>
        /// 商品實體資料表。
        /// </summary>
        internal class product
        {
            /// <summary>
            /// 商品編號。
            /// </summary>
            public int product_id { get; set; }

            /// <summary>
            /// 商品名稱。
            /// </summary>
            public string product_name { get; set; }

            /// <summary>
            /// 商品描述。
            /// </summary>
            public string product_desc { get; set; }

            /// <summary>
            /// 供應商。
            /// </summary>
            public int product_provider { get; set; }
        }

        /// <summary>
        /// 商品價格實體資料表
        /// </summary>
        internal class price
        {
            /// <summary>
            /// 目錄編號。
            /// </summary>
            public int category_id { get; set; }

            /// <summary>
            /// 商品編號。
            /// </summary>
            public int product_id { get; set; }

            /// <summary>
            /// 商品成本。
            /// </summary>
            public decimal product_cost { get; set; }

            /// <summary>
            /// 商品訂價。
            /// </summary>
            public decimal product_price { get; set; }
        }

        static Repository()
        {
            Categories = new List<category>(10);
            Products = new List<product>(20);
            Prices = new List<price>(50);

            CreateSampleData();
        }

        private static void CreateSampleData()
        {
            Categories.Add(new category { category_store = 1, category_id = 1001, category_name = "電視" });
            Categories.Add(new category { category_store = 1, category_id = 1002, category_name = "投影機" });
            Categories.Add(new category { category_store = 1, category_id = 1003, category_name = "音響" });
            Categories.Add(new category { category_store = 2, category_id = 2001, category_name = "家庭劇院" });
            Categories.Add(new category { category_store = 2, category_id = 2002, category_name = "樂活美食" });

            Products.Add(new product { product_id = 1, product_name = "24吋液晶電視", product_provider = 1 });
            Products.Add(new product { product_id = 2, product_name = "27吋液晶電視", product_provider = 1 });
            Products.Add(new product { product_id = 3, product_name = "31吋液晶電視", product_provider = 1 });

            Prices.Add(new price { category_id = 1001, product_id = 1, product_cost = 1000m, product_price = 1500m });
            Prices.Add(new price { category_id = 1001, product_id = 2, product_cost = 1200m, product_price = 1700m });
            Prices.Add(new price { category_id = 1001, product_id = 3, product_cost = 1400m, product_price = 1900m });

            Prices.Add(new price { category_id = 2001, product_id = 1, product_cost = 800m, product_price = 1490m });
        }

        internal static ICollection<category> Categories { get; private set; }

        internal static ICollection<product> Products { get; private set; }

        internal static ICollection<price> Prices { get; private set; }
    }
}
