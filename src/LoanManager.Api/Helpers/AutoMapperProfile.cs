using AutoMapper;
using LoanManager.Api.Models.Request;
using LoanManager.Application.Models.DTO;

namespace LoanManager.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateGameRequestDto, GameDto>();
            CreateMap<CreateFriendRequestDto, FriendDto>();
            CreateMap<CreateLoanRequestDto, LoanDto>();
        }
    }
}
