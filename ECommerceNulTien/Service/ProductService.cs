using ECommerceNulTien.Model;
using ECommerceNulTien.Repository.Interfaces;
using ECommerceNulTien.Service.Interfaces;

namespace ECommerceNulTien.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task UpdateLocalStorage(List<Product> products)
        {
            if (products == null)
                return;

            foreach (var p in products)
            {
                await _productRepository.UpdateProductQuantity(p);
            }
        }
    }
}
