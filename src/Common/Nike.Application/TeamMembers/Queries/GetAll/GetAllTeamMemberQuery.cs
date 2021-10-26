using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nike.Application.Common.Interfaces;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Nike.Application.TeamMembers.Queries.GetAll
{
    public class GetAllTeamMemBerQuery : IRequestWrapper<List<TeamMemberDto>>
    {

    }

    public class GetAllTeamMemBerQueryHandler : IRequestHandlerWrapper<GetAllTeamMemBerQuery, List<TeamMemberDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllTeamMemBerQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<TeamMemberDto>>> Handle(GetAllTeamMemBerQuery request, CancellationToken cancellationToken)
        {
            List<TeamMemberDto> list = await _context.TeamMembers
                .ProjectToType<TeamMemberDto>(_mapper.Config)
                .ToListAsync(cancellationToken);

            return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<TeamMemberDto>>(ServiceError.NotFound);
        }

    }
}
