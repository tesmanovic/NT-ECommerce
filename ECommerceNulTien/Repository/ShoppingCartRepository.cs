using ECommerceNulTien.Model;
using ECommerceNulTien.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceNulTien.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ECommerceDbContext _context;
        public ShoppingCartRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<ShoppingCart?> GetShoppingCartByCustomerId(int customerId)
        {
            var shoppingCart = await _context.ShoptingCarts.Where(sc => sc.CustomerId == customerId).Include(sp => sp.Items).ThenInclude(i => i.Product).FirstOrDefaultAsync();
            return shoppingCart;
        }
        public async Task<bool> AddShoppingItem(int customerId, ShoppingCartItem shoppingCartItem)
        {
            var shoppingCart = _context.ShoptingCarts.Where(sc => sc.CustomerId == customerId).Include(sc => sc.Items).ThenInclude(i => i.Product).FirstOrDefault();

            shoppingCart ??= new Model.ShoppingCart()
            {
                ModificationDate = DateTime.Now,
                CustomerId = Convert.ToInt32(customerId),
                Items = new List<Model.ShoppingCartItem>()
            };

            var itemsCondition = shoppingCart.Items.Where(i => i.Product.Id == shoppingCartItem.Product.Id);
            var productPrice = shoppingCartItem.Product.Price;

            if (itemsCondition.Any())
            {
                var existingItem = itemsCondition.FirstOrDefault();
                shoppingCart.Total -= existingItem.Quantity * productPrice;
                existingItem.Quantity = shoppingCartItem.Quantity;
            }
            else
            {
                shoppingCart.Items.Add(shoppingCartItem);
            }
            shoppingCart.Total += shoppingCartItem.Quantity * productPrice;

            _context.Update(shoppingCart);
            var saved = await _context.SaveChangesAsync();
            return saved == 1;
        }

        public async Task RemoveShoppingCartItems(int customerId)
        {
            var shoppingCart = await _context.ShoptingCarts.Where(sc => sc.CustomerId == customerId).Include(sc => sc.Items).FirstOrDefaultAsync();

            _context.ShoptingCartItems.RemoveRange(shoppingCart.Items);
            shoppingCart.Total = 0;
            shoppingCart.ModificationDate = DateTime.Now;
            _context.ShoptingCarts.Update(shoppingCart);
            await _context.SaveChangesAsync();
        }
    }
}
