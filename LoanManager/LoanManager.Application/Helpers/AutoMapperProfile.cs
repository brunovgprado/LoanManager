using AutoMapper;
using LoanManager.Application.Models.DTO;
using LoanManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanManager.Application.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
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
