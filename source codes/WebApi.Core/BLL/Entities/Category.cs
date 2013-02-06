namespace WebApi.BLL.Entities
{
    public class Category
    {
        public int Id { get; set; }

        /// <summary>
        /// Use to identify which store can sell this product.
        /// </summary>
        public StoreEnum Store { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}