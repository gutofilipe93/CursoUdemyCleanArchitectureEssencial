using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            var products = await _productService.GetProductsAsync();
            if(products == null)
                return NotFound("Products not found");

            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if(product == null)
                return NotFound("Product not found");

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDto productDto)
        {
            if(productDto == null)
                return BadRequest("Invalid data");

            await _productService.AddAsync(productDto);
            return new CreatedAtRouteResult("GetProduct", new {id = productDto.Id}, productDto);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id,[FromBody] ProductDto productDto)
        {            
            if(productDto == null)
                return BadRequest("Invalid data");

            if(id != productDto.Id)
                return BadRequest();

            await _productService.UpdateAsync(productDto);
            return Ok(productDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {            
            var product = await _productService.GetByIdAsync(id);
            if(product == null)
                return NotFound("Product not found");

            await _productService.RemoveAsync(id);
            return Ok(product);
        }

    }
}