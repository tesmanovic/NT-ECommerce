using ECommerceNulTien.Model;
using ECommerceNulTien.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceNulTien.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceDbContext _context;

        public ProductRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductById(int productId)
        {
            var product = await _context.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();
            return product;
        }

        public async Task UpdateProductQuantity(Product product)
        {
            var entity = _context.Products.Where(p => p.Id == Convert.ToInt32(product.Id)).FirstOrDefault();
            entity.Quantity = product.Quantity;
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
