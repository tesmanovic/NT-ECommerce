using ECommerceNulTien.Model;
using ECommerceNulTien.Model.Dto.Response;

namespace ECommerceNulTien.Service.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDetailsDto> CreateOrder(int customerId, Address address, string phoneNumber);
    }
}