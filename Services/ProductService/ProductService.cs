using Microsoft.EntityFrameworkCore;
using TodoWebService.Data;
using TodoWebService.Models.DTOs.Pagination;
using TodoWebService.Models.DTOs.Product;
using TodoWebService.Models.DTOs.Todo;
using TodoWebService.Models.Entities;

namespace TodoWebService.Services.ProductService
{
    public class ProductService : IProductService
    {
        public readonly TodoDbContext _context;

        public ProductService(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<ProductDto?> GetProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            return product is not null
                ? new ProductDto(
                    id: product.Id,
                    title: product.Title,
                    description: product.Description,
                    price: product.Price
                    )
                : null;
        }

        public async Task<ProductDto> ChangeProduct(int id, double price)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            return product is not null
                ? new ProductDto(
                    id: product.Id,
                    title: product.Title,
                    description: product.Description,
                    price: price
                    )
                : null;
        }

        public async Task<ProductDto> CreateProduct(string userId, CreateProductRequest request)
        {
            var product = new Product
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                UserId = userId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ProductDto(product.Id, product.Title, product.Description, product.Price);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product is not null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }



        public async Task<List<ProductDto>> SortingProducts(bool sort)
        {
            List<Product> products;
            if (sort)
            {
                products = await _context.Products.OrderBy(p => p.Price).ToListAsync();
            }
            else
            {
                products = await _context.Products.OrderByDescending(p => p.Price).ToListAsync();
            }
            List<ProductDto> listProducts = new();
            foreach (var product in products)
            {
                listProducts.Add(new ProductDto(id: product.Id, title: product.Title, description: product.Description, price: product.Price));
            }
            return listProducts;
        }

        public async Task<IEnumerable<Product>> FilterProduct(string name)
        {
            var products = await _context.Products.Where(p => p.Title.StartsWith(name)).ToListAsync();
            if (products is null)
                return null;

            return products;

        }
    }
}
