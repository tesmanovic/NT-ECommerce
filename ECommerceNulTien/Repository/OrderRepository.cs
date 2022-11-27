using ECommerceNulTien.Model;
using ECommerceNulTien.Repository.Interfaces;

namespace ECommerceNulTien.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ECommerceDbContext _context;
        public OrderRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.Id;

        }
    }
}
