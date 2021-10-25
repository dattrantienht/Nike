using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nike.Application.Common.Interfaces;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nike.Application.ApplicationUser.Queries.GetUser
{
    public class GetUserByTokenQuery : IRequestWrapper<ApplicationUserDto>
    {
        public string Token { get; set; }
    }

    public class GetUserByTokenQueryHandler : IRequestHandlerWrapper<GetUserByTokenQuery, ApplicationUserDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IIdentityService _identityService;

        public GetUserByTokenQueryHandler(IApplicationDbContext context, 
            IMapper mapper, 
            IConfiguration configuration,
            IIdentityService identityService)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _identityService = identityService;
        }

        public async Task<ServiceResult<ApplicationUserDto>> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserByToken(request.Token);

            return user != null ? ServiceResult.Success(user) : ServiceResult.Failed<ApplicationUserDto>(ServiceError.NotFound);
        }
    }
}
