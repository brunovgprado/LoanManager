using AutoMapper;
using FluentValidation;
using LoanManager.Application.Shared;
using LoanManager.Auth.Exceptions;
using LoanManager.Auth.Interfaces.Repository;
using LoanManager.Auth.Interfaces.Services;
using LoanManager.Auth.Models;
using LoanManager.Auth.Validators;
using System;
using System.Threading.Tasks;

namespace LoanManager.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserValidator _userValidator;
        private readonly IAuthRepository _repository;
        private readonly IMapper _mapper;
        private readonly KeyHasherService _hasherService;
        private readonly TokenService _tokenService;

        public AuthService(
            IAuthRepository repository, 
            IMapper mapper,
            UserValidator userValidator,
            KeyHasherService hasherService,
            TokenService tokenService
            )
        {
            _repository = repository;
            _mapper = mapper;
            _userValidator = userValidator;
            _hasherService = hasherService;
            _tokenService = tokenService;
        }

        public async Task<Response<UserResponse>> Authenticate(UserCredentials credentials)
        {
            var response = new Response<UserResponse>();
            try
            {
                await _userValidator.ValidateAndThrowAsync(credentials);
                
                var userEmail = _mapper.Map<User>(credentials);
                
                userEmail.Email = userEmail.Email.ToLower();
                
                var userAccount = await _repository.GetUser(userEmail);
                
                if (userAccount is null)
                    throw new UserNotFoundException();
                
                var passwordMatch =_hasherService.VerifyKey(credentials.Password, userAccount.Password);
                
                if (!passwordMatch)
                    throw new UserNotFoundException();
                
                var userResponse = _mapper.Map<UserResponse>(userAccount);
                
                userResponse.Token = _tokenService.GenerateToken(userResponse);
                return response.SetResult(userResponse);

            }
            catch (ValidationException ex)
            {
                return response.SetRequestValidationError(ex);
            }
            catch (UserNotFoundException ex)
            {
                return response.SetError(ex.Message, ResponseKind.Unauthorized);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(ex.Message);
            }
        }

        public async Task<Response<UserResponse>> CreateAccount(UserCredentials credentials)
        {
            var response = new Response<UserResponse>();
            try
            {
                await _userValidator.ValidateAndThrowAsync(credentials);
                
                var userAccount = _mapper.Map<User>(credentials);
                
                var userAlreadyExists = await _repository.CheckIfUserAlreadyExistis(userAccount);

                if (userAlreadyExists)
                    throw new EmailAdressAlreadyRegistredException();
                
                userAccount.Id = Guid.NewGuid();
                
                userAccount.Email = userAccount.Email.ToLower();
                
                userAccount.Password = _hasherService.EncriptKey(userAccount.Password);
                
                await _repository.CreateAccount(userAccount);
                
                var userResponse = new UserResponse { Email = credentials.Email };
                
                userResponse.Token = _tokenService.GenerateToken(userResponse);
                return response.SetResult(userResponse);

            }
            catch (ValidationException ex)
            {
                return response.SetRequestValidationError(ex);
            }
            catch (EmailAdressAlreadyRegistredException ex)
            {
                return response.SetBadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return response.SetInternalServerError(ex.Message);
            }
        }
    }
}
