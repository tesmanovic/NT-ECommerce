using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceNulTien.Model
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public List<OrderItem>? Items { get; set; }
        [ForeignKey("FK_Order_Customer_CustomerId")]
        public int CustomerId { get; set; }
        public OrderDetails? OrderDetails { get; set; }
    }
}
