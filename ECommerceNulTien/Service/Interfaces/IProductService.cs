using ECommerceNulTien.Model;

namespace ECommerceNulTien.Service.Interfaces
{
    public interface IProductService
    {
        Task UpdateLocalStorage(List<Product> products);
    }
}