using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared.Models
{
    public class ProductVariant
    {
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        // use JsonIgnore to avoid circular reference
        [JsonIgnore]
        public Product? Product { get; set; }
        [ForeignKey(nameof(ProductType))]
        public int ProductTypeId { get; set; }
        public virtual ProductType? ProductType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; }
        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
