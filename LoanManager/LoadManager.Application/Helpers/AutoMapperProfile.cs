﻿using AutoMapper;
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
        }
    }
}
