using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoriesContracts.RepositoriesContracts;
using ServicesContracts;
using ServicesContracts.DTOs;
using ServicesContracts.Mappers;

namespace Services.Services;

public class CategoryService : ICategoryService
{
	private readonly ICategoryRepository _categoryRepository;

	private readonly ILogger<CategoryService> _logger;

	public CategoryService(ICategoryRepository CategoryRepository, ILogger<CategoryService> logger)
	{
		_logger = logger;
		_categoryRepository = CategoryRepository;
	}

	public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
	{
		try
		{
			return (await _categoryRepository.GetAllCategoriesAsync()).Select((c) => CategoryMapper.ToDTO(c));
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al obtener las categorías de la base de datos.");
		}
		catch (SqlException ex4)
		{
			SqlException ex5 = ex4;
			SqlException ex6 = ex5;
			_logger.LogError(((Exception)(object)ex6).Message, ex6);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentNullException ex7)
		{
			ArgumentNullException ex8 = ex7;
			_logger.LogError(ex8.Message, ex8);
			throw new ArgumentNullException("La categoría no puede ser nula.");
		}
		catch (Exception ex9)
		{
			Exception ex10 = ex9;
			_logger.LogError(ex10.Message, ex10);
			throw new Exception("Ocurrió un error inesperado al obtener las categorías.");
		}
	}

	public async Task<CategoryDTO> GetCategoryAsync(Guid id)
	{
		try
		{
			Category category = await _categoryRepository.GetCategoryByIdAsync(id);
			if (category == null)
			{
			}
			return CategoryMapper.ToDTO(category);
		}
		catch (DbUpdateException ex)
		{
			DbUpdateException ex2 = ex;
			DbUpdateException ex3 = ex2;
			_logger.LogError(((Exception)(object)ex3).Message, ex3);
			throw new Exception("Error al obtener la categoría de la base de datos.");
		}
		catch (SqlException ex4)
		{
			SqlException ex5 = ex4;
			SqlException ex6 = ex5;
			_logger.LogError(((Exception)(object)ex6).Message, ex6);
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentException ex7)
		{
			ArgumentException ex8 = ex7;
			_logger.LogError(ex8.Message, ex8);
			throw new ArgumentException(ex8.Message);
		}
		catch (Exception ex9)
		{
			Exception ex10 = ex9;
			_logger.LogError(ex10.Message, ex10);
			throw new Exception("Ocurrió un error inesperado al obtener la categoría.");
		}
	}
}
