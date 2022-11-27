using System.Text.Json.Serialization;

namespace ECommerceNulTien.Model.Dto.Response
{
    public class OrderDetailsDto
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }
        [JsonPropertyName("totalAmount")]
        public double TotalAmount { get; set; }
        [JsonPropertyName("appliedDiscount")]
        public double AppliedDiscount { get; set; }

    }
}
