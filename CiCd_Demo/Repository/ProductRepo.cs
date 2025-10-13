using CiCd_Demo.DataContext;
using CiCd_Demo.DTO;
using CiCd_Demo.Model;
using CiCd_Demo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CiCd_Demo.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly ApplicationDbContext _db;

        public ProductRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> CreateProducts(CreateProducts products)
        {
            try
            {
                var newProduct = new Product()
                {
                    Name = products.Name,
                    Price = products.Price,
                    Category = products.Category
                };

                await _db.Products.AddAsync(newProduct);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Optional: Log the exception (depending on your logger)
                // _logger.LogError(ex, "Error occurred while creating a product");

                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Product>> GetProduct()
        {
            try
            {
                var prodDetails = await _db.Products.ToListAsync();
                return prodDetails;
            }
            catch (Exception ex)
            {
                // Optional: Log exception
                // _logger.LogError(ex, "Error occurred while fetching products.");

                Console.WriteLine($"Error: {ex.Message}");
                return new List<Product>(); // Return empty list on error
            }
        }


        public async Task<Product> GetProductById(int id)
        {
            try
            {
                var prodDetails = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (prodDetails != null)
                {
                    return prodDetails;
                }

                return null; // If not found, return null
            }
            catch (Exception ex)
            {
                // Optional: Log exception
                // _logger.LogError(ex, "Error occurred while fetching product by ID.");

                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                var prodDetails = await _db.Products.FirstOrDefaultAsync(x => x.Id == product.Id);

                if (prodDetails == null)
                    return false;

                prodDetails.Name = product.Name;
                prodDetails.Price = product.Price;
                prodDetails.Category = product.Category;

                _db.Products.Update(prodDetails);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Optional: log the exception
                // _logger.LogError(ex, "Error occurred while updating product.");

                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

    }
}
