using System.Threading.Tasks;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using Nike.Application.Villages.Queries.GetVillagesWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Nike.Api.Controllers
{
    [Authorize]
    public class VillagesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<PaginatedList<VillageDto>>>> GetAllVillagesWithPagination(GetAllVillagesWithPaginationQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
