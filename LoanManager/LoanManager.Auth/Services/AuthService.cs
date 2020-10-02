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

        public AuthService(
            IAuthRepository repository, 
            IMapper mapper,
            UserValidator userValidator,
            KeyHasherService hasherService
            )
        {
            _repository = repository;
            _mapper = mapper;
            _userValidator = userValidator;
            _hasherService = hasherService;
        }

        public async Task<Response<UserResponse>> Authenticate(UserCredentials credentials)
        {
            var response = new Response<UserResponse>();
            try
            {
                // Validating object properties
                await _userValidator.ValidateAndThrowAsync(credentials);

                // Mapping UserCredentials to User entity
                var userEntity = _mapper.Map<User>(credentials);

                // Trying to get User account with match email address
                var user = await _repository.GetUser(userEntity);

                // Throwing exception when email address don't match any users
                if (user == null)
                    throw new UserNotFoundException();

                // Validating password
                var passwordNotMatch =_hasherService.VerifyKey(credentials.Password, user.Password);

                // Throwing exception when password not match
                if (passwordNotMatch)
                    throw new UserNotFoundException();

                // Mapping User entity to UserResponse object
                var userResponse = _mapper.Map<UserResponse>(user);

                // Generating token and return UserResponse object
                userResponse.Token = TokenService.GenerateToken(userResponse);
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
                // Validating object properties
                await _userValidator.ValidateAndThrowAsync(credentials);

                // Mapping UserCredentials entity to User object
                var userEntity = _mapper.Map<User>(credentials);

                // Verifying if existis some user account with the same email address
                var userAlreadyExistis = await _repository.CheckIfUserAlreadyExistis(userEntity);

                // Throwing exception when existis some user account with the same email address
                if (userAlreadyExistis)
                    throw new EmailAdressAlreadyRegistredException();

                // Encrypting password before persist
                userEntity.Password = _hasherService.EncriptKey(userEntity.Password);

                // Persisting User
                await _repository.CreateAccount(userEntity);

                // Creating a new UserResponse instance an setting email address
                var userResponse = new UserResponse { Email = credentials.Email };

                // Generating token and return UserResponse object
                userResponse.Token = TokenService.GenerateToken(userResponse);
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
