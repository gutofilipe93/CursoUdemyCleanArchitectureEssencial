using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            if(categories == null)
                return NotFound("Categories not found");

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if(category == null)
                return NotFound("Category not found");

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDto categoryDto)
        {
            if(categoryDto == null)
                return BadRequest("Invalid data");

            await _categoryService.AddAsync(categoryDto);
            return new CreatedAtRouteResult("GetCategory", new {id = categoryDto.Id}, categoryDto);
        }

        [HttpPut]
        public async Task<ActionResult> Put(int id,[FromBody] CategoryDto categoryDto)
        {            
            if(categoryDto == null)
                return BadRequest("Invalid data");

            if(id != categoryDto.Id)
                return BadRequest();

            await _categoryService.UpdateAsync(categoryDto);
            return Ok(categoryDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {            
            var category = await _categoryService.GetByIdAsync(id);
            if(category == null)
                return NotFound("Category not found");

            await _categoryService.RemoveAsync(id);
            return Ok(category);
        }
    }
}