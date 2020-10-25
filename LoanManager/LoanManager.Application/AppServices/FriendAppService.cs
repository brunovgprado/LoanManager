using AutoMapper;
using FluentValidation;
using LoanManager.Application.Interfaces.AppServices;
using LoanManager.Application.Models.DTO;
using LoanManager.Application.Properties;
using LoanManager.Application.Shared;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Exceptions;
using LoanManager.Domain.Interfaces.DomainServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanManager.Application.AppServices
{
    public class FriendAppService : IFriendAppService
    {
        private readonly IFriendDomainService _friendDomainService;
        private readonly IMapper _mapper;

        public FriendAppService(
            IFriendDomainService friendDomainService,
            IMapper mapper
            )
        {
            _friendDomainService = friendDomainService;
            _mapper = mapper;
        }

        public async Task<Response<object>> Create(FriendDto friend)
        {
            var response = new Response<Object>();
            try
            {
                var friendEntity = _mapper.Map<Friend>(friend);
                var result = await _friendDomainService.CreateAsync(friendEntity);
                return response.SetResult(new { Id = result });
            }
            catch (ValidationException ex)
            {
                return response.SetRequestValidationError(ex);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGeneratingLoan);
            }
        }

        public async Task<Response<bool>> Delete(Guid id)
        {
            var response = new Response<bool>();
            try
            {
                await _friendDomainService.DeleteAsync(id);
                return response.SetResult(true);
            }
            catch (EntityNotExistsException)
            {
                return response.SetNotFound(Resources.CantFounFriendWithGivenId);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGeneratingLoan);
            }
        }

        public async Task<Response<FriendDto>> Get(Guid id)
        {
            var response = new Response<FriendDto>();
            try
            {
                var result = await _friendDomainService.ReadAsync(id);
                return response.SetResult(_mapper.Map<FriendDto>(result));
            }
            catch (EntityNotExistsException)
            {
                return response.SetNotFound(Resources.CantFounFriendWithGivenId);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGeneratingLoan);
            }
        }

        public async Task<Response<IEnumerable<FriendDto>>> GetAll(int offset, int limit)
        {
            var response = new Response<IEnumerable<FriendDto>>();
            try
            {
                var result = await _friendDomainService.ReadAllAsync(offset, limit);
                return response.SetResult(_mapper.Map<IEnumerable<FriendDto>>(result));
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGeneratingLoan);
            }
        }

        public async Task<Response<bool>> Update(FriendDto friend)
        {
            var response = new Response<bool>();
            try
            {
                // Mapping FriendDto to Friend entity
                var friendEntity = _mapper.Map<Friend>(friend);

                // Persisting and returning result
                await _friendDomainService.Update(friendEntity);
                return response.SetResult(true);
            }
            catch (EntityNotExistsException)
            {
                return response.SetNotFound(Resources.CantFounFriendWithGivenId);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(Resources.UnexpectedErrorWhileGeneratingLoan);
            }
        }
    }
}
