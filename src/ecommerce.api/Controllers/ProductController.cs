using ecommerce.application.Product.Commands;
using ecommerce.application.Product.Queries;
using ecommerce.core.Entities;
using ecommerce.core.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NG.API.Extensions;
using NG.HttpResponse;

namespace ecommerce.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IMediator mediator, ILogger<ProductController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<ProductController> _logger = logger;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<ProductResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AppProblemDetails))]
        public async Task<IActionResult> GetAsync()
        {
            var toReturn = await _mediator.Send(new ProductGetAllQuery());
            return Ok(new ApiResponse<ICollection<ProductResponse>> { Response = toReturn });
        }
        [HttpGet("search/{searchtext}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<ProductResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AppProblemDetails))]
        public async Task<IActionResult> SearchText(string searchtext)
        {
            var toReturn = await _mediator.Send(new ProductSearchByTextQuery { SearchText = searchtext });
            return Ok(new ApiResponse<ICollection<ProductResponse>> { Response = toReturn });
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"Get product by id: {id}");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiResponse<Product<Guid>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(AppProblemDetails))]
        public async Task<IActionResult> PostAsync(AddProductCommand toAdd)
        {
            var toReturn = await _mediator.Send(toAdd);
            return Created("Url", new ApiResponse<bool> { Response = true });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            return Ok($"Update product by id: {id}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Delete product by id: {id}");
        }
    }
}
