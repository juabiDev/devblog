using DevBlog.DTOs;
using DevBlog.Entities;

namespace DevBlog.Mappers
{
    public static class TagMapper
    {
        public static TagDTO ToDTO(Tag tag)
        {
            return new TagDTO
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }
        public static Tag ToEntity(TagDTO tagDTO)
        {
            return new Tag
            {
                Id = tagDTO.Id,
                Name = tagDTO.Name
            };
        }
    }
}
