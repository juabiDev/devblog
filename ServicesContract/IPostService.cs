using DevBlog.DTOs;
using DevBlog.Entities;

namespace DevBlog.ServicesContract
{
    public interface IPostService
    {
        Task CreatePost(CreatePostRequest post);
        Task<Post> UpdatePost(Post post);
        Task<Post> DeletePost(Guid id);
        Task<Post> GetPostById(Guid id);
        Task<List<PostDTO>> GetAllPosts();
        Task<List<Post>> GetPostsByCategory(Guid categoryId);
        Task<List<Post>> GetPostsByTag(Guid tagId);
        Task<List<Post>> GetPostsByAuthor(Guid authorId);
    }
}
