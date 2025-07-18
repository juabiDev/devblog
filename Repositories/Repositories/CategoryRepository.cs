using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;
using Repositories.DBContext;
using RepositoriesContracts.RepositoriesContracts;

namespace Repositories.Repositories;

public class CategoryRepository : ICategoryRepository
{
	private readonly BlogDbContext _db;

	private readonly ResiliencePipeline _resiliencePipeline;

	public CategoryRepository(BlogDbContext db, ResiliencePipeline resiliencePipeline)
	{
		_db = db;
		_resiliencePipeline = resiliencePipeline;
	}

	public async Task AddCategoryAsync(Category category)
	{
		try
		{
			await _resiliencePipeline.ExecuteAsync(async delegate(CancellationToken token)
			{
				await _db.Category.AddAsync(category, token);
				await _db.SaveChangesAsync(token);
			}, default);
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al agregar la categoría en la base de datos.");
		}
		catch (SqlException)
		{
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentNullException)
		{
			throw new ArgumentNullException("La categoría no puede ser nula.");
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al agregar la categoría.");
		}
	}

	public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async (token) => await _db.Category.AsSplitQuery().ToListAsync(token), default);
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al obtener las categorías de la base de datos.");
		}
		catch (SqlException)
		{
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentNullException)
		{
			throw new ArgumentNullException("La categoría no puede ser nula.");
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al obtener las categorías.");
		}
	}

	public async Task<Category> GetCategoryByIdAsync(Guid id)
	{
		try
		{
			return await _resiliencePipeline.ExecuteAsync(async (token) => await _db.Category.AsSplitQuery().FirstOrDefaultAsync((Expression<Func<Category, bool>>)((c) => c.Id == id), token), default);
		}
		catch (DbUpdateException)
		{
			throw new Exception("Error al obtener la categoría de la base de datos.");
		}
		catch (SqlException)
		{
			throw new Exception("Error al conectar la base de datos");
		}
		catch (ArgumentNullException)
		{
			throw new ArgumentNullException("La categoría no puede ser nula.");
		}
		catch (ArgumentException)
		{
			throw new ArgumentException("La categoría no existe.");
		}
		catch (Exception)
		{
			throw new Exception("Ocurrió un error inesperado al obtener la categoría.");
		}
	}
}
