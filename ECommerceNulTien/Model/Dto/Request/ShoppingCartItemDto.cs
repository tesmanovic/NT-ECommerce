using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECommerceNulTien.Model.Dto.Request
{
    public class ShoppingCartItemDto
    {
        [Required]
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }
        [Required]
        [JsonPropertyName("quantity")]
        [DefaultValue(1)]
        [Range(1, Int32.MaxValue, ErrorMessage = "Quantity field must be greater then 0")]
        public int Quantity { get; set; }
    }
}
