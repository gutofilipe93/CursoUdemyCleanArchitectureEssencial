using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(IMapper mapper, IMediator mediator)
        {            
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task AddAsync(ProductDto productDto)
        {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDto);
            await _mediator.Send(productCreateCommand);
        }

        public async Task<ProductDto> GetByIdAsync(int? id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id.Value));
            return _mapper.Map<ProductDto>(result);
        }     

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var result = await _mediator.Send(new GetProductsQuery());
            return _mapper.Map<IEnumerable<ProductDto>>(result);
        }

        public async Task RemoveAsync(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);
            await _mediator.Send(productRemoveCommand);
        }

        public async Task UpdateAsync(ProductDto productDto)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDto);
            await _mediator.Send(productUpdateCommand);
        }
    }
}
