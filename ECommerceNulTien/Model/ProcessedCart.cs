namespace ECommerceNulTien.Model
{
    public class ProcessedCart
    {
        public List<OrderItem> OrderItems { get; set; }
        public List<Product> ProductsLocal { get; set; }
        public List<Product> ProductsExternal { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }
    }
}
