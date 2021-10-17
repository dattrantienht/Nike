using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nike.Application.Common.Interfaces;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Nike.Application.Products.Queries.GetById
{
    public class GetProductByIdQuery : IRequestWrapper<ProductDto>
    {
        public int ProductId { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandlerWrapper<GetProductByIdQuery, ProductDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var Product = await _context.Products
                .Where(x => x.Id == request.ProductId)
                .ProjectToType<ProductDto>(_mapper.Config)
                .FirstOrDefaultAsync(cancellationToken);

            return Product != null ? ServiceResult.Success(Product) : ServiceResult.Failed<ProductDto>(ServiceError.NotFound);
        }
    }
}
