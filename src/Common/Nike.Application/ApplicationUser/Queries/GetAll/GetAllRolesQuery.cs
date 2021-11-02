using Microsoft.AspNetCore.Identity;
using Nike.Application.Common.Interfaces;
using Nike.Application.Common.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nike.Application.ApplicationUser.Queries.GetAll
{
    public class GetAllRolesQuery : IRequestWrapper<List<IdentityRole>>
    {
    }

    public class GetAllRolesQueryHandler : IRequestHandlerWrapper<GetAllRolesQuery, List<IdentityRole>>
    {
        private readonly IIdentityService _identityService;

        public GetAllRolesQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ServiceResult<List<IdentityRole>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            List<IdentityRole> listRoles = await _identityService.GetAllRoles();
            return listRoles.Count > 0 ? ServiceResult.Success(listRoles) : ServiceResult.Failed<List<IdentityRole>>(ServiceError.NotFound);
        }
    }
}