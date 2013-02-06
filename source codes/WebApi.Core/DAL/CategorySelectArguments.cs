using WebApi.BLL.Entities;
namespace WebApi.DAL
{
    public sealed class CategorySelectArguments
    {
        public int? Id { get; set; }

        public StoreEnum? Store { get; set; }

        public int? ParentId { get; set; }
    }
}