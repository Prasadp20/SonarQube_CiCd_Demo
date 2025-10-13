using System.ComponentModel.DataAnnotations;

namespace CiCd_Demo.DTO
{
    public class CreateProducts
    {

        [Required, StringLength(150)]
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Category { get; set; }

    }
}
