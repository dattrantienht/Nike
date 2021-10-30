using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nike.Application.Products.Commands.Create;
using Nike.Application.Products.Queries.GetAll;
using Nike.Application.Products.Queries.GetById;
using Nike.Application.Products.Commands.Update;
using Nike.Application.Products.Commands.Delete;
using Nike.Application.ProductCategories.Commands.Update;
using Nike.Application.Products.Queries.GetList;

namespace Nike.Api.Controllers
{
    public class ProductsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<List<ProductDto>>>> GetAllProducts(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllProductsQuery(), cancellationToken));
        }

        [HttpGet("list")]
        public async Task<ActionResult<ServiceResult<List<ProductDto>>>> GetAllListProducts(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllListProductQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<ProductDto>>> GetProductCategoryById(int id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery { ProductId = id }));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ServiceResult<ProductDto>>> Create(CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ServiceResult<ProductDto>>> Update(UpdateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResult<ProductDto>>> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteProductCommand { Id = id }));
        }
    }
}
