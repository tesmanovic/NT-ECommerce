using ECommerceNulTien.Exceptions;
using ECommerceNulTien.Helpers;
using ECommerceNulTien.Model;
using ECommerceNulTien.Model.Dto.Response;
using ECommerceNulTien.Repository.Interfaces;
using ECommerceNulTien.Service.Interfaces;

namespace ECommerceNulTien.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ISupplierService _supplierService;
        private readonly IProductService _productService;
        private readonly ICustomerRepository _customerRepository;
        public OrderService(IOrderRepository orderRepository,
            IShoppingCartRepository shoppingCartRepository,
            ISupplierService supplierService,
            IProductService productService,
            ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _supplierService = supplierService;
            _productService = productService;
            _customerRepository = customerRepository;
        }

        public async Task<OrderDetailsDto> CreateOrder(int customerId, Address address, string phoneNumber)
        {
            if (address == null || string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentException($"Parametar phone number is required");

            var customer = await _customerRepository.GetCustomerById (customerId);

            if (customer == null)
                throw new KeyNotFoundException(string.Format($"Customer with id {{0}} does not exist.", customerId));

            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByCustomerId(customerId);

            if (shoppingCart == null || shoppingCart?.Items?.Count == 0)
                throw new KeyNotFoundException(string.Format($"Shopping cart for customer with id {{0}} is empty.", customerId));

            var processedCart = ProcessShoppingCart(shoppingCart.Items);
            var discountAmount = DiscountHelper.Calculate(processedCart.TotalAmount, phoneNumber);
            var orderDetails = CreateOrderDetails(address, phoneNumber);

            await _productService.UpdateLocalStorage(processedCart.ProductsLocal);

            _supplierService.UpdateExternalStorage(processedCart.ProductsExternal);

            var order = new Order()
            {
                CreationDate = DateTime.Now,
                ModificationDate = DateTime.Now,
                CustomerId = Convert.ToInt32(customerId),
                TotalAmount = Convert.ToDouble(processedCart.TotalAmount) - discountAmount,
                DiscountAmount = discountAmount,
                OrderDetails = orderDetails,
                Items = processedCart.OrderItems
            };

            var orderId = await _orderRepository.AddOrder(order);
            await _shoppingCartRepository.RemoveShoppingCartItems(customerId);

            var response = new OrderDetailsDto()
            {
                OrderId = orderId,
                TotalAmount = order.TotalAmount,
                AppliedDiscount = discountAmount
            };

            return response;
        }

        private ProcessedCart ProcessShoppingCart(List<ShoppingCartItem> items)
        {
            var result = new ProcessedCart()
            {
                OrderItems = new List<OrderItem>(),
                ProductsLocal = new List<Product>(),
                ProductsExternal = new List<Product>()
            };

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    CreationDate = DateTime.Now,
                    ModificationDate = DateTime.Now,
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity
                };

                if (item.Product.Quantity >= item.Quantity)
                    result.ProductsLocal.Add(new Product()
                    {
                        Id = item.Product.Id,
                        Quantity = item.Product.Quantity - item.Quantity
                    });
                else
                {
                    var isAvailable = _supplierService.IsProductAvailable(item.Product, item.Quantity);
                    if (!isAvailable)
                        throw new ResourceNotAvailableException(string.Format($"Requested quantity of product {{0}} is not available anymore.", item.Product.ProductName));

                    result.ProductsLocal.Add(new Product()
                    {
                        Id = item.Product.Id,
                        Quantity = item.Quantity
                    });
                }
                result.OrderItems.Add(orderItem);
            }

            result.TotalAmount = (double)items.Select(i => i.Product?.Price * i.Quantity).Sum();
            return result;
        }
        private static OrderDetails CreateOrderDetails(Address address, string phoneNumber)
        {
            return new OrderDetails()
            {
                City = address.City,
                Street = address.Street,
                HouseNumber = address.HouseNumber,
                PhoneNumber = phoneNumber
            };
        }
    }
}
