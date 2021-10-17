using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nike.Application.Common.Interfaces;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Nike.Application.ProductCategories.Queries.GetProductCategoryById
{
    public class GetProductCategoryByIdQuery : IRequestWrapper<ProductCategoryDto>
    {
        public int ProductCategoryId { get; set; }
    }

    public class GetProductCategoryByIdQueryHandler : IRequestHandlerWrapper<GetProductCategoryByIdQuery, ProductCategoryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductCategoryByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<ProductCategoryDto>> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var productCategory = await _context.ProductCategories
                .Where(x => x.Id == request.ProductCategoryId)
                .Include(d => d.Products)
                .ProjectToType<ProductCategoryDto>(_mapper.Config)
                .FirstOrDefaultAsync(cancellationToken);

            return productCategory != null ? ServiceResult.Success(productCategory) : ServiceResult.Failed<ProductCategoryDto>(ServiceError.NotFound);
        }
    }
}
