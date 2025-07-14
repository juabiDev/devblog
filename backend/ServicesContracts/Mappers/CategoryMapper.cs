using Entities;
using ServicesContracts.DTOs;

namespace ServicesContracts.Mappers;

public static class CategoryMapper
{
	public static CategoryDTO ToDTO(Category category)
	{
		return new CategoryDTO
		{
			Id = category.Id,
			Name = category.Name
		};
	}

	public static Category ToEntity(CategoryDTO category)
	{
		return new Category
		{
			Id = category.Id,
			Name = category.Name
		};
	}
}
