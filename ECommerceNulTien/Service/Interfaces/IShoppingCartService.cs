using ECommerceNulTien.Model.Dto.Response;

namespace ECommerceNulTien.Service.Interfaces
{
    public interface IShoppingCartService
    {
        Task<List<ShoppingCartDto>> GetShoppingCartByCustomerId(int customerId);
    }
}