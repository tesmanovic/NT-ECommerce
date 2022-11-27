using ECommerceNulTien.Exceptions;
using ECommerceNulTien.Model;
using ECommerceNulTien.Repository.Interfaces;
using ECommerceNulTien.Service.Interfaces;

namespace ECommerceNulTien.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISupplierService _supplierService;
        public CustomerService(ICustomerRepository customerRepository,
            IShoppingCartRepository shoppingCartRepository,
            IProductRepository productRepository,
            ISupplierService supplierService)
        {
            _customerRepository = customerRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _supplierService = supplierService;
        }

        public async Task AddProductToShoppingCart(int customerId, int productId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException();

            var customer = await _customerRepository.GetCustomerById(customerId);

            if (customer == null)
                throw new KeyNotFoundException(string.Format($"Customer with id {{0}} does not exist.", customerId));

            var product = await _productRepository.GetProductById(productId);

            if (product == null)
                throw new KeyNotFoundException(string.Format($"Product with id {{0}} does not exist.", productId));

            var onStock = IsProductOnStock(product, quantity);

            if (!onStock)
                throw new ResourceNotAvailableException(string.Format($"Requested quantity of product {{0}} is not available right now.", product.ProductName));

            var shoppingItem = new ShoppingCartItem()
            {
                Quantity = quantity,
                Product = product,
            };

            await _shoppingCartRepository.AddShoppingItem(customerId, shoppingItem);
        }

        private bool IsProductOnStock(Product product, int quantity)
        {
            if (product.Quantity >= quantity)
                return true;

            return _supplierService.IsProductAvailable(product, quantity);

        }
    }
}
