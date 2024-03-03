namespace TodoWebService.Models.DTOs.Product
{
    public class ProductDto
    {
        public ProductDto(int id, string title, string description, double price)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
