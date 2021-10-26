using System.Threading.Tasks;
using Nike.Application.ApplicationUser.Queries.GetToken;
using Nike.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Nike.Application.Dto;
using Nike.Application.ApplicationUser.Commands.CheckUser;

namespace Nike.Api.Controllers
{
    public class LoginController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<ServiceResult<LoginResponse>>> Create(GetTokenQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("check")]
        public async Task<ActionResult<ServiceResult<ApplicationUserDto>>> Create(CheckUserByTokenCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
