using AutoMapper;
using LoanManager.Application.Models.DTO;
using LoanManager.Domain.Entities;

namespace LoanManager.Application.Helpers
{
    public class AppAutoMapperProfile : Profile
    {
        public AppAutoMapperProfile()
        {
            CreateMap<GameDto, Game>();
            CreateMap<Game, GameDto>();

            CreateMap<FriendDto, Friend>();
            CreateMap<Friend, FriendDto>();

            CreateMap<LoanDto, Loan>();
            CreateMap<Loan, LoanDto>();
        }
    }
}
