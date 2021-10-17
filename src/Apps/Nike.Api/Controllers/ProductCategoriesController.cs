using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nike.Application.ProductCategories.Commands.Create;
using Nike.Application.ProductCategories.Queries.GetAll;
using Nike.Application.ProductCategories.Queries.GetById;
using Nike.Application.ProductCategories.Commands.Update;

namespace Nike.Api.Controllers
{
    public class ProductCategoriesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<List<ProductCategoryDto>>>> GetAllProductCategories(CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(new GetAllProductCategoriesQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<ProductCategoryDto>>> GetProductCategoryById(int id)
        {
            return Ok(await Mediator.Send(new GetProductCategoryByIdQuery { ProductCategoryId = id }));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ServiceResult<ProductCategoryDto>>> Create(CreateProductCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ServiceResult<ProductCategoryDto>>> Update(UpdateProductCategoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<ServiceResult<CityDto>>> Delete(int id)
        //{
        //    return Ok(await Mediator.Send(new DeleteCityCommand { Id = id }));
        //}
    }
}
