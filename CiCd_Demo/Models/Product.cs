using System.ComponentModel.DataAnnotations;

namespace CiCd_Demo.Model
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Category { get; set; }
    }
}
