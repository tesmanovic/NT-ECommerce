using ECommerceNulTien.Model;
using ECommerceNulTien.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceNulTien.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private ECommerceDbContext _context;
        public CustomerRepository(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<Customer> GetCustomerById(int customerId)
        {
            var customer = await _context.Customers.Where(c => c.Id == Convert.ToInt32(customerId)).FirstOrDefaultAsync();
            return customer;

        }
    }
}
