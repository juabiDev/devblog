﻿using DevBlog.DTOs;
using DevBlog.Entities;

namespace DevBlog.ServicesContract
{
    public interface IPostService
    {
        Task CreatePost(CreatePostRequest post);
        Task<Post> UpdatePost(Post post);
        Task DeletePost(Guid id);
        Task<Post> GetPostById(Guid id);
        Task<List<PostDTO>> GetAllPosts();
        Task<List<PostDTO>> GetPostsByCategory(Guid categoryId);
        Task<List<PostDTO>> GetPostsByTag(Guid tagId);
        Task<List<PostDTO>> GetPostsByAuthor(Guid authorId);
    }
}
