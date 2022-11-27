using ECommerceNulTien.Model;

namespace ECommerceNulTien.Service.Interfaces
{
    public interface ISupplierService
    {
        bool IsProductAvailable(Product product, int quantity);
        void UpdateExternalStorage(List<Product> products);
    }
}