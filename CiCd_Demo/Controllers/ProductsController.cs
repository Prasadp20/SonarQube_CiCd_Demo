using CiCd_Demo.DataContext;
using CiCd_Demo.DTO;
using CiCd_Demo.Model;
using CiCd_Demo.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IProductRepo _productRepo;
    public ProductsController(ApplicationDbContext db, IProductRepo productRepo)
    {
        this._db = db;
        this._productRepo = productRepo;
    }

    [HttpPost("CreateProduct")]
    public async Task<IActionResult> CreateProducts(CreateProducts products)
    {
        if (products == null)
        {
            return BadRequest("Product data cannot be null.");
        }

        var isCreated = await _productRepo.CreateProducts(products);

        if (isCreated)
        {
            return Ok(new { message = "Product created successfully." });
        }
        else
        {
            return StatusCode(500, new { message = "An error occurred while creating the product." });
        }
    }


    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _productRepo.GetProduct();

        if (products == null || products.Count == 0)
            return NotFound(new { message = "No products found." });

        return Ok(products);
    }


    [HttpGet("GetProductById/{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productRepo.GetProductById(id);

        if (product == null)
            return NotFound(new { message = $"Product with ID {id} not found." });

        return Ok(product);
    }


    [HttpPut("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct([FromBody] Product product)
    {
        if (product == null)
            return BadRequest("Invalid product data.");

        var isUpdated = await _productRepo.UpdateProduct(product);

        if (isUpdated)
            return Ok(new { message = "Product updated successfully." });
        else
            return NotFound(new { message = $"Product with ID {product.Id} not found or update failed." });
    }


    //[HttpPut("{id:int}")]
    //public async Task<IActionResult> Update(int id, Product product)
    //{
    //    if (id != product.Id) return BadRequest();
    //    _db.Entry(product).State = EntityState.Modified;
    //    await _db.SaveChangesAsync();
    //    return NoContent();
    //}

    //[HttpDelete("{id:int}")]
    //public async Task<IActionResult> Delete(int id)
    //{
    //    var p = await _db.Products.FindAsync(id);
    //    if (p == null) return NotFound();
    //    _db.Products.Remove(p);
    //    await _db.SaveChangesAsync();
    //    return NoContent();
    //}
}
