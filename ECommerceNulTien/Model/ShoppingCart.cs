using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceNulTien.Model
{
    public class ShoppingCart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double Total { get; set; }
        public DateTime ModificationDate { get; set; }
        public List<ShoppingCartItem>? Items { get; set; }
        [ForeignKey("FK_ShoppingCart_Customer_CustomerId")]
        public int CustomerId { get; set; }
    }
}
