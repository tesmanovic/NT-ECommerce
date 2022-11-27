using ECommerceNulTien.Model;
using ECommerceNulTien.Service.Interfaces;

namespace ECommerceNulTien.Service
{
    public class SupplierService : ISupplierService
    {
        private IHttpClientFactory _clientFactory { get; set; }
        public SupplierService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public bool IsProductAvailable(Product product, int quantity)
        {
            int value = new System.Random().Next(0, 100);
            return value <= 50;
        }

        public void UpdateExternalStorage(List<Product> products)
        {
            return;
        }
    }
}
