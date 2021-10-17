using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Nike.Application.Common.Interfaces;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Nike.Application.Products.Queries.GetAll
{
    public class GetAllProductsQuery : IRequestWrapper<List<ProductDto>>
    {

    }

    public class GetAllProductsQueryHandler : IRequestHandlerWrapper<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            List<ProductDto> list = await _context.Products
                .ProjectToType<ProductDto>(_mapper.Config)
                .ToListAsync(cancellationToken);

            return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<ProductDto>>(ServiceError.NotFound);
        }

    }
}
