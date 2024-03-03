using TodoWebService.Models.DTOs.Pagination;
using TodoWebService.Models.DTOs.Product;
using TodoWebService.Models.DTOs.Todo;
using TodoWebService.Models.Entities;

namespace TodoWebService.Services.ProductService
{
    public interface IProductService
    {
        Task<ProductDto?> GetProduct(int id);
        Task<ProductDto> CreateProduct(string userId, CreateProductRequest request);
        Task<ProductDto> ChangeProduct(int id, double price);
        Task<bool> DeleteProduct(int id);
        Task<List<ProductDto>> SortingProducts(bool sort);
        public Task<IEnumerable<Product>> FilterProduct(string name);
    }
}
