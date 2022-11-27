namespace ECommerceNulTien.Service.Interfaces
{
    public interface ICustomerService
    {
        Task AddProductToShoppingCart(int customerId, int productId, int quantity);
    }
}