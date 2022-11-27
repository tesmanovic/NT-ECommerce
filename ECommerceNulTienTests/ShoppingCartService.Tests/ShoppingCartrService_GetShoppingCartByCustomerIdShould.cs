using ECommerceNulTien;
using ECommerceNulTien.Model.Dto.Response;
using ECommerceNulTien.Repository;
using ECommerceNulTien.Repository.Interfaces;
using ECommerceNulTien.Service;
using Microsoft.EntityFrameworkCore;

namespace ECommerceNulTienTests.Services.Tests
{
    [TestClass]
    public class ShoppingCartrService_GetShoppingCartByCustomerIdShould
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICustomerRepository _customerRepository;

        public ShoppingCartrService_GetShoppingCartByCustomerIdShould()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ECommerceDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ECommerceDbContext(optionsBuilder.Options);
            context.Database.EnsureCreated();
            _shoppingCartRepository = new ShoppingCartRepository(context);
            _customerRepository = new CustomerRepository(context);
        }
        [TestMethod]
        public async Task InputNegative_ThrowsKeyNotFoundException()
        {
            var service = new ShoppingCartService(_shoppingCartRepository, _customerRepository);
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () => await service.GetShoppingCartByCustomerId(-1));
        }

        [TestMethod]
        public async Task InputExistingData_ProperType()
        {
            var service = new ShoppingCartService(_shoppingCartRepository, _customerRepository);
            var response = await service.GetShoppingCartByCustomerId(123456);
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(List<ShoppingCartDto>));
        }

        [TestMethod]
        public async Task InputNotExistingData_ThrowsKeyNotFoundException()
        {
            var service = new ShoppingCartService(_shoppingCartRepository, _customerRepository);
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () => await service.GetShoppingCartByCustomerId(Int32.MaxValue));
        }
    }
}