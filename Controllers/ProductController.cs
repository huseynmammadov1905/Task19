using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using TodoWebService.Data;
using TodoWebService.Models.DTOs.Pagination;
using TodoWebService.Models.DTOs.Product;
using TodoWebService.Models.DTOs.Todo;
using TodoWebService.Models.Entities;
using TodoWebService.Providers;
using TodoWebService.Services.ProductService;

namespace TodoWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IRequestUserProvider _provider;
        private readonly IMemoryCache _memoryCache;


        public ProductController(IProductService productService, IRequestUserProvider provider, IMemoryCache memoryCache)
        {
            _productService = productService;
            _provider = provider;
            _memoryCache = memoryCache;

        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            UserInfo? userInfo = _provider.GetUserInfo();
            var item = await _productService.GetProduct(id);

            if (item is not null)
            {



                if (_memoryCache.TryGetValue<TodoItemDto>(userInfo!.Id, out var todoItem))
                {
                    return Ok(todoItem);
                }
                else
                {
                    await Task.Delay(3000);


                    _memoryCache.Set(
                        key: userInfo.Id,
                        value: item,
                        options: new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });

                    return Ok(item);
                }
            }

            else return NotFound();



            return item is not null
                ? item
                : NotFound();
        }

       
        [HttpPost("create")]
        public async Task<ActionResult<ProductDto>> Create(CreateProductRequest request)
        {
            var userInfo = _provider.GetUserInfo();
            var result = await _productService.CreateProduct(userInfo!.Id, request);
            return result is not null ? result : BadRequest("Something went wrong");
        }

        [HttpPost("change/{id}")]
        public async Task<ActionResult<ProductDto>> ChangeTodo(ChangeProductRequest request, int id)
        {
            var userInfo = _provider.GetUserInfo();
            var result = await _productService.ChangeProduct(id, request.Price);
            return result is not null ? result : BadRequest("Something went wrong");
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<bool>> DeleteTodo(int id)
        {

            var result = await _productService.DeleteProduct(id);
            if (!result)
                return BadRequest();
            return Ok();
        }

        [HttpPost("sorting/{sort}")]
        public async Task<List<ProductDto>> Sort(bool sort)
        {
            var result = await _productService.SortingProducts(sort);
            return result;
        }

        [HttpGet("filter")]
        public async Task<IEnumerable<Product>> FilterByName([FromQuery] string name)
        {
            return await _productService.FilterProduct(name);
        }
    }
}
