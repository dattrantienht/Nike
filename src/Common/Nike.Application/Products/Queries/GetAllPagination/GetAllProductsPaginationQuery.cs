using System.Linq;
using MapsterMapper;
using Nike.Application.Common.Interfaces;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nike.Application.Common.Mapping;

namespace Nike.Application.Products.Queries.GetAllPagination
{
    public class GetAllProductsPaginationQuery : IRequestWrapper<PaginatedList<ListProductDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string keyword { get; set; }
    }

    public class GetAllProductsPaginationQueryHandler : IRequestHandlerWrapper<GetAllProductsPaginationQuery, PaginatedList<ListProductDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllProductsPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResult<PaginatedList<ListProductDto>>> Handle(GetAllProductsPaginationQuery request, CancellationToken cancellationToken)
        {

            if (!string.IsNullOrEmpty(request.keyword))
            {
                var query = await (from a in _context.Products
                                   join b in _context.ProductCategories
                                   on a.ProductCategoryId equals b.Id
                                   where a.Name.ToLower().Contains(request.keyword.ToLower())
                                   select new ListProductDto()
                                   {
                                       Id = a.Id,
                                       Name = a.Name,
                                       Image = a.Image,
                                       Price = a.Price,
                                       ProductCategoryName = b.Name
                                   }).PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
                return ServiceResult.Success(query);
            }
            else
            {
                var query = await (from a in _context.Products
                                   join b in _context.ProductCategories
                                   on a.ProductCategoryId equals b.Id
                                   select new ListProductDto()
                                   {
                                       Id = a.Id,
                                       Name = a.Name,
                                       Image = a.Image,
                                       Price = a.Price,
                                       ProductCategoryName = b.Name
                                   }).PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
                return ServiceResult.Success(query);
            }
                

            return  ServiceResult.Failed<PaginatedList<ListProductDto>>(ServiceError.NotFound);
        }
    }
}
