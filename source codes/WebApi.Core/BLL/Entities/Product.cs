using System.ComponentModel.DataAnnotations;
using WebApi.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Data.Services.Common;

namespace WebApi.BLL.Entities
{
    [DataContract]
    public class Product
    {
        [Required, DataMember(IsRequired = true)]
        public int? Id { get; set; }

        [Required, DataMember]
        public string Name { get; set; }

        [DataMember]
        public Category Category { get; set; }

        [DataMember]
        public decimal Cost { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [Range(1, 100), DataMember]
        public double Weight { get; set; }

        [DataMember]
        public int SupplierId { get; set; }
    }
}