using CiCd_Demo.DTO;
using CiCd_Demo.Model;

namespace CiCd_Demo.Repository.Interfaces
{
    public interface IProductRepo
    {
        Task<bool> CreateProducts(CreateProducts products);
        Task<List<Product>> GetProduct();
        Task<Product> GetProductById(int id);
        Task<bool> UpdateProduct(Product product);
    }
}
