using System.Threading;
using System.Threading.Tasks;
using Nike.Application.Common.Interfaces;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using Nike.Domain.Entities;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;

namespace Nike.Application.ApplicationUser.Commands.Create
{
    public class CreateUserCommand : IRequestWrapper<ApplicationUserDto>
    {
        public string username { get; set; }
        public string password { get; set; }
        public string roleId { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandlerWrapper<CreateUserCommand, ApplicationUserDto>
    {
        private readonly IIdentityService _identityService;

        public CreateUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<ServiceResult<ApplicationUserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _identityService.CreateUserAsync(request.username, request.password);
            var newUser = await _identityService.CheckUserPassword(request.username, request.password);
            if(newUser != null)
            {
                await _identityService.AddUserToRole(newUser.Id, request.roleId);
            }
            return newUser != null ? ServiceResult.Success(newUser) : ServiceResult.Failed<ApplicationUserDto>(ServiceError.UserFailedToCreate);
        }
    }
}
