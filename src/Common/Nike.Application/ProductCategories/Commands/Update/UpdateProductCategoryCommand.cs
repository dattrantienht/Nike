using System.Threading;
using System.Threading.Tasks;
using Nike.Application.Common.Exceptions;
using Nike.Application.Common.Interfaces;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using Nike.Domain.Entities;
using MapsterMapper;

namespace Nike.Application.ProductCategories.Commands.Update
{
    public class UpdateProductCategoryCommand : IRequestWrapper<ProductCategoryDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class UpdateProductCategoryCommandHandler : IRequestHandlerWrapper<UpdateProductCategoryCommand, ProductCategoryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateProductCategoryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<ProductCategoryDto>> Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ProductCategories.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ProductCategory), request.Id);
            }
            if (!string.IsNullOrEmpty(request.Name))
                entity.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<ProductCategoryDto>(entity));
        }
    }
}
