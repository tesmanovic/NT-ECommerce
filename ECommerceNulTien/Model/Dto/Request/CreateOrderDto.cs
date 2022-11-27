using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ECommerceNulTien.Model.Dto.Request
{
    public class CreateOrderDto
    {
        [Required]
        [JsonPropertyName("address")]
        public Address Address { get; set; }
        [JsonPropertyName("phoneNumber")]
        [Required]
        [RegularExpression("^\\d+$", ErrorMessage = "Phone number only accepts numbers.")]
        public string PhoneNumber { get; set; }
    }
}
