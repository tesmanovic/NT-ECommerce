using ECommerceNulTien.Model;

namespace ECommerceNulTien.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<int> AddOrder(Order order);
    }
}