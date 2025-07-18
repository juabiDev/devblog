using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Entities;

namespace RepositoriesContracts.RepositoriesContracts;

public interface ICategoryRepository
{
	Task AddCategoryAsync(Category category);

	Task<Category> GetCategoryByIdAsync(Guid id);

	Task<IEnumerable<Category>> GetAllCategoriesAsync();
}
