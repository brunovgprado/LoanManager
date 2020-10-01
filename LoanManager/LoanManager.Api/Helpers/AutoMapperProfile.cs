using AutoMapper;
using LoanManager.Api.Models.Request;
using LoanManager.Application.Models.DTO;

namespace LoanManager.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateGameRequest, GameDto>();
            CreateMap<CreateFriendRequest, FriendDto>();
            CreateMap<CreateLoanRequest, LoanDto>();
        }
    }
}
