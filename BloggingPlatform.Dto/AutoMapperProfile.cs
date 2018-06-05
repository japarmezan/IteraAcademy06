using AutoMapper;
using BloggingPlatform.Db.Model;
using System.Linq;

namespace BloggingPlatform.Dto
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Authors, AuthorDto>().ReverseMap();

            CreateMap<Posts, PostDto>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.PostText))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.PostsCategories.Select(pc => pc.Category)));

            CreateMap<PostDto, Posts>()
                .ForMember(dest => dest.PostText, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.PostsCategories, opt => opt.Ignore())
                .ForMember(dest => dest.Author, opt => opt.Ignore());
                        
            CreateMap<PostsCategories, PostCategoryDto>().ReverseMap();

            CreateMap<CategoryDto, Categories>().ReverseMap();

            CreateMap<CategoryDto, Categories>().ForMember(c => c.PostsCategories, opt => opt.Ignore());

            CreateMap<CommentDto, Comments>()
                .ForMember(dest => dest.Author, opt => opt.Ignore())
                .ForMember(dest => dest.Post, opt => opt.Ignore())
                .ForMember(dest => dest.CommentText, opt => opt.MapFrom(src => src.Content));

            CreateMap<Comments, CommentDto>()
                .ForMember(dest => dest.Author, opt=> opt.MapFrom(src => src.Author.Username))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.CommentText));            
        }
    }
}
