using ECommerceNulTien.Model;

namespace ECommerceNulTien.Repository.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<bool> AddShoppingItem(int customerId, ShoppingCartItem shoppingCartItem);
        Task<ShoppingCart?> GetShoppingCartByCustomerId(int customerId);
        Task RemoveShoppingCartItems(int customerId);
    }
}