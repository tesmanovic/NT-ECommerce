using System.Text.Json.Serialization;

namespace ECommerceNulTien.Model.Dto.Response
{
    public class ShoppingCartDto
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }
        [JsonPropertyName("productName")]
        public string ProductName { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("unitPrice")]
        public double UnitPrice { get; set; }
    }
}
