using ECommerceNulTien.Model.Dto.Request;
using ECommerceNulTien.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceNulTien.Controllers
{
    [ApiController]
    public class ShoppingCartController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILogger<ShoppingCartController> _logger;
        public ShoppingCartController(ICustomerService customerService,
            ILogger<ShoppingCartController> logger,
             IShoppingCartService shoppingCartService)
        {
            _customerService = customerService;
            _logger = logger;
            _shoppingCartService = shoppingCartService;
        }

        [HttpPost]
        [Route("customers/{customerId}/cart")]
        public async Task<IActionResult> AddProductToShoppingCart([FromRoute] string customerId, [FromBody] ShoppingCartItemDto dto)
        {
            if (!Int32.TryParse(customerId, out int customerIdParsed))
                throw new ArgumentException("Invalid custumerId parameter.");

            await _customerService.AddProductToShoppingCart(customerIdParsed, dto.ProductId, dto.Quantity);

            return new OkObjectResult(new { message = "Product successfully added." });
        }

        [HttpGet]
        [Route("customers/{customerId}/cart")]
        public async Task<IActionResult> GetShoppingCartByCustomerId([FromRoute] string customerId)
        {
            if (!Int32.TryParse(customerId, out int customerIdParsed))
                throw new ArgumentException("Invalid custumerId parameter.");

            var response = await _shoppingCartService.GetShoppingCartByCustomerId(customerIdParsed);

            return new OkObjectResult(new { items = response });
        }
    }
}
