using ECommerceNulTien.Model.Dto.Request;
using ECommerceNulTien.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceNulTien.Controllers
{
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        [Route("customers/{customerId}/orders")]
        public async Task<IActionResult> CreateOrder([FromRoute] string customerId, [FromBody] CreateOrderDto dto)
        {
            if (!Int32.TryParse(customerId, out int customerIdParsed))
                throw new ArgumentException("Invalid custumerId parameter.");

            var response = await _orderService.CreateOrder(customerIdParsed, dto.Address, dto.PhoneNumber);
            return new OkObjectResult(new { orderDetails = response });
        }
    }
}
