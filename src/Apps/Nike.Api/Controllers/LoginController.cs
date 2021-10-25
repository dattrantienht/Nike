using System.Threading.Tasks;
using Nike.Application.ApplicationUser.Queries.GetToken;
using Nike.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Nike.Application.Dto;
using Nike.Application.ApplicationUser.Queries.GetUser;

namespace Nike.Api.Controllers
{
    public class LoginController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<ServiceResult<LoginResponse>>> Create(GetTokenQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{token}")]
        public async Task<ActionResult<ServiceResult<ApplicationUserDto>>> GetUserByToken(string token)
        {
            return Ok(await Mediator.Send(new GetUserByTokenQuery { Token = token }));
        }
    }
}
