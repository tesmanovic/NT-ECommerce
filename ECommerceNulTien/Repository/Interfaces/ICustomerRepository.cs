using ECommerceNulTien.Model;

namespace ECommerceNulTien.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerById(int customerId);
    }
}
