using System.Threading;
using System.Threading.Tasks;
using Nike.Application.Common.Interfaces;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using Nike.Domain.Entities;
using Nike.Domain.Event;
using MapsterMapper;

namespace Nike.Application.ProductCategories.Commands.Create
{
    public class CreateProductCategoryCommand : IRequestWrapper<ProductCategoryDto>
    {
        public string Name { get; set; }
    }

    public class CreateProductCategoryCommandHandler : IRequestHandlerWrapper<CreateProductCategoryCommand, ProductCategoryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateProductCategoryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<ProductCategoryDto>> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new ProductCategory
            {
                Name = request.Name
            };

            await _context.ProductCategories.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<ProductCategoryDto>(entity));
        }
    }
}
