using Nike.Application.Common.Interfaces;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nike.Application.ApplicationUser.Queries.GetAll
{
    public class GetAllUsersQuery : IRequestWrapper<List<ApplicationUserDto>>
    {
    }

    public class GetAllUsersQueryHandler : IRequestHandlerWrapper<GetAllUsersQuery, List<ApplicationUserDto>>
    {
        private readonly IIdentityService _identityService;

        public GetAllUsersQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ServiceResult<List<ApplicationUserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<ApplicationUserDto> listUser = await _identityService.GetAllUsers();
            return listUser.Count > 0 ? ServiceResult.Success(listUser) : ServiceResult.Failed<List<ApplicationUserDto>>(ServiceError.NotFound);
        }
    }
}