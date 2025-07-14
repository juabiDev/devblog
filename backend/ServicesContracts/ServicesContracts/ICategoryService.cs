using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServicesContracts.DTOs;

namespace ServicesContracts;

public interface ICategoryService
{
	Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();

	Task<CategoryDTO> GetCategoryAsync(Guid id);
}
