using Nike.Application.Common.Interfaces;
using Nike.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Nike.Application.ApplicationUser.Commands.Delete
{
    public class DeleteUserCommand : IRequestWrapper<Result>
    {
        public string userId { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandlerWrapper<DeleteUserCommand, Result>
    {
        private readonly IIdentityService _identityService;

        public DeleteUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ServiceResult<Result>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.DeleteUserAsync(request.userId);
            return ServiceResult.Success(result);
        }
    }
}