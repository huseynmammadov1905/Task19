namespace TodoWebService.Models.DTOs.Product
{
    public class ChangeProductRequest
    {
        [System.ComponentModel.DataAnnotations.Required]
        public double Price { get; set; }
    }
}
