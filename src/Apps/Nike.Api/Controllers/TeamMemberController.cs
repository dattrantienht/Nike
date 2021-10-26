using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nike.Application.TeamMembers.Queries.GetAll;

namespace Nike.Api.Controllers
{
    [Authorize]
    public class TeamMemberController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<List<TeamMemberDto>>>> GetAllProductCategories(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllTeamMemBerQuery(), cancellationToken));
        }

    }
}
