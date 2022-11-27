using ECommerceNulTien;
using ECommerceNulTien.Model;
using ECommerceNulTien.Repository;
using ECommerceNulTien.Repository.Interfaces;
using ECommerceNulTien.Service;
using ECommerceNulTien.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ECommerceNulTienTests.Services.Tests
{
    [TestClass]
    public class OrderService_CreateOrderShould
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductService _productService;
        private Mock<ISupplierService> _supplierService;
        public OrderService_CreateOrderShould()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ECommerceDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new ECommerceDbContext(optionsBuilder.Options);
            context.Database.EnsureCreated();

            _orderRepository = new OrderRepository(context);
            _shoppingCartRepository = new ShoppingCartRepository(context);
            _productService = new ProductService(new ProductRepository(context));
            _supplierService = new Mock<ISupplierService>();
        }

        [TestMethod]
        [DataRow(-1)]
        public async Task InputCustomerIdNegativ_ThrowsKeyNotFoundException(int customerId)
        {
            _supplierService.SetReturnsDefault(true);
            var orderService = new OrderService(_orderRepository, _shoppingCartRepository, _supplierService.Object, _productService);
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () => await orderService.CreateOrder(customerId, new Address(), "0656011065"));
        }

        [TestMethod]
        [DataRow(null)]
        public async Task InputAddressNull_ThrowsArgumentException(Address address)
        {
            _supplierService.SetReturnsDefault(true);
            var orderService = new OrderService(_orderRepository, _shoppingCartRepository, _supplierService.Object, _productService);
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await orderService.CreateOrder(12345, address, "0656011065"));
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public async Task InputPhoneInvalid_ThrowsArgumentException(string phone)
        {
            _supplierService.SetReturnsDefault(true);
            var orderService = new OrderService(_orderRepository, _shoppingCartRepository, _supplierService.Object, _productService);
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () => await orderService.CreateOrder(12345, new Address(), phone));
        }

        [TestMethod]
        public async Task InputValidDateEmptyShoppingCart_ThrowsKeyNotFoundException()
        {
            _supplierService.SetReturnsDefault(false);
            var orderService = new OrderService(_orderRepository, _shoppingCartRepository, _supplierService.Object, _productService);
            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () => await orderService.CreateOrder(12345, new Address(), "0656011065"));
        }
    }
}
