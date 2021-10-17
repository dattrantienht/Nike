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

namespace Nike.Application.Products.Commands.Delete
{
    public class DeleteProductCommand : IRequestWrapper<ProductDto>
    {
        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandlerWrapper<DeleteProductCommand, ProductDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<ProductDto>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            _context.Products.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<ProductDto>(entity));
        }
    }
}
