using ECommerceNulTien.Model;

namespace ECommerceNulTien.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int productId);
        Task UpdateProductQuantity(Product p);
    }
}