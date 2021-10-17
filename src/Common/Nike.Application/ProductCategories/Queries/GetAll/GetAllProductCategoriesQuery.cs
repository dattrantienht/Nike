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

namespace Nike.Application.ProductCategories.Queries.GetAll
{
    public class GetAllProductCategoriesQuery : IRequestWrapper<List<ProductCategoryDto>>
    {

    }

    public class GetAllProductCategoriesQueryHandler : IRequestHandlerWrapper<GetAllProductCategoriesQuery, List<ProductCategoryDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllProductCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<ProductCategoryDto>>> Handle(GetAllProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            List<ProductCategoryDto> list = await _context.ProductCategories
                .Include(x => x.Products)
                .ProjectToType<ProductCategoryDto>(_mapper.Config)
                .ToListAsync(cancellationToken);

            return list.Count > 0 ? ServiceResult.Success(list) : ServiceResult.Failed<List<ProductCategoryDto>>(ServiceError.NotFound);
        }

    }
}
