namespace TodoWebService.Models.Entities
{
    public class Product :BaseEntity
    {
        public string Title { get; set; }   
        public string Description { get; set; }
        public double Price { get; set; }
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
    }
}
