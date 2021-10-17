using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nike.Application.Common.Exceptions;
using Nike.Application.Common.Interfaces;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using Nike.Domain.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Nike.Application.ProductCategories.Commands.Delete
{
    public class DeleteProductCategoryCommand : IRequestWrapper<ProductCategoryDto>
    {
        public int Id { get; set; }
    }

    public class DeleteProductCategoryCommandHandler : IRequestHandlerWrapper<DeleteProductCategoryCommand, ProductCategoryDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteProductCategoryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<ProductCategoryDto>> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ProductCategories
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ProductCategory), request.Id);
            }

            _context.ProductCategories.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<ProductCategoryDto>(entity));
        }
    }
}
