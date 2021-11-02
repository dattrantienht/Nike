using System.Threading.Tasks;
using Nike.Application.ApplicationUser.Queries.GetToken;
using Nike.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Nike.Application.Dto;
using Nike.Application.ApplicationUser.Commands.CheckUser;
using Nike.Application.ApplicationUser.Commands.Create;
using System.Threading;
using System.Collections.Generic;
using Nike.Application.ApplicationUser.Queries.GetAll;
using Nike.Application.ApplicationUser.Commands.Delete;

namespace Nike.Api.Controllers
{
    public class LoginController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<ServiceResult<LoginResponse>>> Login(GetTokenQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("check")]
        public async Task<ActionResult<ServiceResult<ApplicationUserDto>>> Check(CheckUserByTokenCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("adduser")]
        public async Task<ActionResult<ServiceResult<LoginResponse>>> CreateUser(CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResult<List<ApplicationUserDto>>>> GetAllUsers(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllUsersQuery(), cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResult<Result>>> Delete(string id)
        {
            return Ok(await Mediator.Send(new DeleteUserCommand { userId = id }));
        }
    }
}
