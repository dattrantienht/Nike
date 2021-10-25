using Nike.Application.Common.Interfaces;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Nike.Application.ApplicationUser.Commands.CheckUser
{
    public class CheckUserByTokenCommand : IRequestWrapper<ApplicationUserDto>
    {
        public string Token { get; set; }
    }

    public class CheckUserByTokenCommandHandler : IRequestHandlerWrapper<CheckUserByTokenCommand, ApplicationUserDto>
    {
        private readonly IIdentityService _identityService;

        public CheckUserByTokenCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ServiceResult<ApplicationUserDto>> Handle(CheckUserByTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserByToken(request.Token);

            return user != null ? ServiceResult.Success(user) : ServiceResult.Failed<ApplicationUserDto>(ServiceError.NotFound);
        }
    }
}