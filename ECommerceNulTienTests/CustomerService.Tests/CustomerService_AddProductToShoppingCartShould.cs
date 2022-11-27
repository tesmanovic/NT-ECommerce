using ECommerceNulTien;
using ECommerceNulTien.Exceptions;
using ECommerceNulTien.Repository;
using ECommerceNulTien.Repository.Interfaces;
using ECommerceNulTien.Service;
using ECommerceNulTien.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ECommerceNulTienTests.Services.Tests
{
    [TestClass]
    public class CustomerService_AddProductToShoppingCartShould
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;
        private Mock<ISupplierService> _supplierService;

        public CustomerService_AddProductToShoppingCartShould()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ECommerceDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ECommerceDbContext(optionsBuilder.Options);
            context.Database.EnsureCreated();

            _customerRepository = new CustomerRepository(context);
            _shoppingCartRepository = new ShoppingCartRepository(context);
            _productRepository = new ProductRepository(context);
            _supplierService = new Mock<ISupplierService>();

        }

        [TestMethod]
        public async Task InputNotExistingCustomer_ThrowsKeyNotFoundException()
        {
            var service = new CustomerService(_customerRepository, _shoppingCartRepository, _productRepository, _supplierService.Object);
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () => await service.AddProductToShoppingCart(-1, -1, 10));
        }

        [TestMethod]
        public async Task InputNotExistingProduct_ThrowsKeyNotFoundException()
        {
            var service = new CustomerService(_customerRepository, _shoppingCartRepository, _productRepository, _supplierService.Object);
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () => await service.AddProductToShoppingCart(12345, -1, 10));
        }

        [TestMethod]
        public async Task InputQuantityZero_ThrowsArgumentException()
        {
            var service = new CustomerService(_customerRepository, _shoppingCartRepository, _productRepository, _supplierService.Object);
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await service.AddProductToShoppingCart(12345, 11112, 0));
        }

        [TestMethod]
        public async Task InputExistingValidData_OnStock()
        {
            _supplierService.SetReturnsDefault(true);
            var service = new CustomerService(_customerRepository, _shoppingCartRepository, _productRepository, _supplierService.Object);
            await service.AddProductToShoppingCart(12345, 11112, 10);
        }

        [TestMethod]
        public async Task InputExistingValidData_ThrowsResourceNotAvailableException()
        {
            _supplierService.SetReturnsDefault(false);
            var service = new CustomerService(_customerRepository, _shoppingCartRepository, _productRepository, _supplierService.Object);
            await Assert.ThrowsExceptionAsync<ResourceNotAvailableException>(async () => await service.AddProductToShoppingCart(12345, 11112, int.MaxValue));

        }
    }
}
