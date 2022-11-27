using ECommerceNulTien;
using ECommerceNulTien.Model;
using ECommerceNulTien.Repository.Interfaces;
using ECommerceNulTien.Service;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ECommerceNulTienTests.Services.Tests
{
    [TestClass]
    public class ProductService_UpdateLocalStorageShould
    {
        private Mock<IProductRepository> _productRepository;
        public ProductService_UpdateLocalStorageShould()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ECommerceDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ECommerceDbContext(optionsBuilder.Options);
            context.Database.EnsureCreated();
            _productRepository = new Mock<IProductRepository>();
        }

        [TestMethod]
        public async Task UpdateLocalStorage_InputNull_DoesNotCallRepository()
        {

            var service = new ProductService(_productRepository.Object);
            await service.UpdateLocalStorage(null);
            var calls = _productRepository.Invocations.Count;
            Assert.AreEqual(calls, 0);

        }

        [TestMethod]
        public async Task UpdateLocalStorage_InputValid_CountCallsToRepository()
        {
            var products = new List<Product>() { new Product(), new Product() };
            var service = new ProductService(_productRepository.Object);
            await service.UpdateLocalStorage(products);
            var calls = _productRepository.Invocations.Count;
            Assert.AreEqual(calls, products.Count);
        }
    }
}
