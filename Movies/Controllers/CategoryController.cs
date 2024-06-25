using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Services;

namespace Movies.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
        private readonly IcategoryService categoryService ;

		public CategoryController(IcategoryService icategoryService)
		{
			categoryService = icategoryService ;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var categories = await categoryService.GetAll();
			return Ok(categories);
		}
		[HttpPost]
		public async Task <IActionResult> CreateAsync([FromBody]CreateCategoryDto dto )

		{
			var category = new Category { Name = dto.Name };
			await categoryService.Add(category);
			return Ok(category);
		}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync(int id,[FromBody]CreateCategoryDto dto)
		{
			var category = await categoryService.Get(id);
			if(category == null)
			{
				return NotFound($"no category Found");
			}
			category.Name = dto.Name;
			categoryService.Update(category);
				return Ok(category);
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var category = await categoryService.Get(id);
			if (category == null)
			{
				return NotFound($"no category Found");
			}
			await categoryService.Delete(category);
			return Ok(category);
		}
	}
}
