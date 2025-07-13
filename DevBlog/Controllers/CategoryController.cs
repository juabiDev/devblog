using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts;

namespace DevBlog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : Controller
{
	private readonly ICategoryService _categoryService;

	public CategoryController(ICategoryService categoryService)
	{
		_categoryService = categoryService;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllCategories()
	{
		try
		{
			return Ok(await _categoryService.GetCategoriesAsync());
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetCategory(Guid id)
	{
		try
		{
			return Ok(await _categoryService.GetCategoryAsync(id));
		}
		catch (ArgumentException ex)
		{
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}
}
