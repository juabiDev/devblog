using DevBlog.DTOs;

namespace DevBlog.ServicesContract
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        Task<CategoryDTO> GetCategoryAsync(Guid id);
    }
}
