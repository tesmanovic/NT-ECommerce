using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceNulTien.Model
{
    public class ShoppingCartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        [ForeignKey("FK_ShoppingCartItem_Product_ProductId")]
        public int ProductId { get; set; }
        [ForeignKey("FK_ShoppingCartItem_ShoppingCart_ShoppingCartId")]
        public int ShoppingCartId { get; set; }
    }
}
