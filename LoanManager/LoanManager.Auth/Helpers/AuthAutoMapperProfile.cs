using AutoMapper;
using LoanManager.Auth.Models;

namespace LoanManager.Auth.Helpers
{
    public class AuthAutoMapperProfile : Profile
    {
        public AuthAutoMapperProfile()
        {
            CreateMap<User, UserCredentials>();
            CreateMap<UserCredentials, User>();

            CreateMap<User, UserResponse>();
            CreateMap<UserResponse, User>();
        }
    }
}
