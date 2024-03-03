using System.ComponentModel.DataAnnotations;

namespace TodoWebService.Models.DTOs.Product
{
    public class CreateProductRequest
    {
        [Required]
        [MinLength(2)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }

    }
}
