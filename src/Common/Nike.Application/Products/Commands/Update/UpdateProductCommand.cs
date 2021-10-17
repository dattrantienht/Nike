using System.Threading;
using System.Threading.Tasks;
using Nike.Application.Common.Exceptions;
using Nike.Application.Common.Interfaces;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using Nike.Domain.Entities;
using MapsterMapper;

namespace Nike.Application.Products.Commands.Update
{
    public class UpdateProductCommand : IRequestWrapper<ProductDto>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int ProductCategoryId { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandlerWrapper<UpdateProductCommand, ProductDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Products.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }
            if (!string.IsNullOrEmpty(request.Name))
                entity.Name = request.Name;
            entity.Image = request.Image;
            entity.Price = request.Price;
            entity.Description = request.Description;
            entity.ProductCategoryId = request.ProductCategoryId;

            await _context.SaveChangesAsync(cancellationToken);

            return ServiceResult.Success(_mapper.Map<ProductDto>(entity));
        }
    }
}
