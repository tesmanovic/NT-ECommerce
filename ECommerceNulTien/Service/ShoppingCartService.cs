using ECommerceNulTien.Model;
using ECommerceNulTien.Model.Dto.Response;
using ECommerceNulTien.Repository.Interfaces;
using ECommerceNulTien.Service.Interfaces;

namespace ECommerceNulTien.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICustomerRepository _customerRepository;
        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, ICustomerRepository customerRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _customerRepository = customerRepository;
        }
        public async Task<List<ShoppingCartDto>> GetShoppingCartByCustomerId(int customerId)
        {

            var customer = await _customerRepository.GetCustomerById(customerId);

            if (customer == null)
                throw new KeyNotFoundException(string.Format($"Customer with id {{0}} does not exist.", customerId));

            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByCustomerId(customerId);

            if (shoppingCart == null || shoppingCart.Items.Count == 0)
                return new List<ShoppingCartDto>();

            var response = GenarateResponseShoppingCart(shoppingCart.Items);

            return response;
        }

        private List<ShoppingCartDto> GenarateResponseShoppingCart(List<ShoppingCartItem> items)
        {
            var list = new List<ShoppingCartDto>();
            foreach (var item in items)
            {
                list.Add(new ShoppingCartDto()
                {
                    ProductId = item.Product.Id,
                    ProductName = item.Product.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                });
            }

            return list;
        }
    }
}
