using AutoMapper;
using todoListApi.Models;


namespace todoListApi.DTOs
{
    public class RegisterRequest
    {
        public required string FullName { get; set; } = string.Empty;
        public required string Email { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;

    }

    public class MapRegisterRequest : Profile
    {
        public MapRegisterRequest()
        {
            CreateMap<RegisterRequest, Users>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }

    public class LoginRequest
    {
        public required string Email { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
    }
}

