using ecommerce.application.Category.Queries;
using ecommerce.core.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NG.API.Extensions;
using NG.HttpResponse;

namespace ecommerce.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(IMediator mediator, ILogger<ProductController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<ProductController> _logger = logger;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ICollection<ProductResponse>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync()
        {
            var toReturn = await _mediator.Send(new CategoryGetAllQuery());
            return Ok(new ApiResponse<ICollection<CategoryResponse>> { Response = toReturn });
        }
    }
}
