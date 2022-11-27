using ECommerceNulTien.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ECommerceNulTien
{
    public class ECommerceDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ShoppingCart> ShoptingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoptingCartItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Filename=ECommerceDB.db", options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map table names
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasOne(e => e.ShoppingCart).WithOne();
                entity.HasData(
                    new Customer()
                    {
                        Id = 12345,
                        FirstName = "Slobodanka",
                        LastName = "Tesmanovic",
                        PhoneNumber = "0656011065",
                        Username = "tesmanov",
                        Password = ""
                    },
                    new Customer()
                    {
                        Id = 123456,
                        FirstName = "UserTest",
                        LastName = "UserTest",
                        PhoneNumber = "0660237482",
                        Username = "user",
                        Password = ""
                    });
            });

            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasData(new Product()
                {
                    Id = 11110,
                    ProductName = "product1",
                    Price = 700.0,
                    Quantity = 100
                },
                    new Product()
                    {
                        Id = 11111,
                        ProductName = "product2",
                        Price = 100.0,
                        Quantity = 5
                    },
                    new Product()
                    {
                        Id = 11112,
                        ProductName = "product3",
                        Price = 1700.0,
                        Quantity = 10
                    },
                    new Product()
                    {
                        Id = 11113,
                        ProductName = "product4",
                        Price = 4000.0,
                        Quantity = 25
                    });
            });

            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.OrderDetails).WithOne();
                entity.HasMany(e => e.Items).WithOne();
            });

            modelBuilder.Entity<OrderDetails>().ToTable("OrderDetails");
            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<OrderItem>().ToTable("OrderItem");
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id); ;
            });

            modelBuilder.Entity<ShoppingCart>().ToTable("ShoppingCart");
            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasMany(e => e.Items).WithOne();
                entity.HasData(new ShoppingCart()
                {
                    Id = 11111,
                    ModificationDate = DateTime.Now,
                    Total = 0.0,
                    Items = new List<ShoppingCartItem>(),
                    CustomerId = 12345
                },
                new ShoppingCart()
                {
                    Id = 11112,
                    ModificationDate = DateTime.Now,
                    Total = 0.0,
                    CustomerId = 123456
                });
            });

            modelBuilder.Entity<ShoppingCartItem>().ToTable("ShoppingCartItem");
            modelBuilder.Entity<ShoppingCartItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasData(new ShoppingCartItem()
                {
                    Id = 11111,
                    ProductId = 11111,
                    ShoppingCartId = 11112,
                    Quantity = 5
                });
            });

            base.OnModelCreating(modelBuilder);
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
        }

    }
}
