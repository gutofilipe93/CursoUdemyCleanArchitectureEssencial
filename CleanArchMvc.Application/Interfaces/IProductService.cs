using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetByIdAsync(int? id);
        Task<ProductDto> GetProductCategoryAsync(int? id);
        Task AddAsync(ProductDto productDto);
        Task UpdateAsync(ProductDto productDto);
        Task RemoveAsync(int? id);
    }
}
