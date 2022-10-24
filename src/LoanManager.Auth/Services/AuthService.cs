using AutoMapper;
using LoanManager.Auth.Interfaces.Repository;
using LoanManager.Auth.Interfaces.Services;
using LoanManager.Auth.Models;
using LoanManager.Auth.Properties;
using LoanManager.Auth.Validators;
using LoanManager.Domain.DomainServices;
using LoanManager.Infrastructure.CrossCutting.Helpers;
using LoanManager.Infrastructure.CrossCutting.NotificationContext;
using System;
using System.Threading.Tasks;

namespace LoanManager.Auth.Services
{
    public class AuthService : BaseDomainService, IAuthService
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
            TokenService tokenService,
            INotificationHandler notificationHandler
            )
            : base(notificationHandler)
        {
            _repository = repository;
            _mapper = mapper;
            _userValidator = userValidator;
            _hasherService = hasherService;
            _tokenService = tokenService;
        }

        public async Task<UserResponse> Authenticate(UserCredentials credentials)
        {
            GuardClauses.IsNotNull(credentials, nameof(credentials));

            var userResponse = new UserResponse();

            if (IsValid(credentials, _userValidator))
            {
                var userEmail = _mapper.Map<User>(credentials);

                userEmail.Email = userEmail.Email.ToLower();

                var userAccount = await _repository.GetUser(userEmail);

                if (userAccount is null)
                {
                    notificationHandler.AddNotification(new Notification("NotFound", string.Format(Resources.UserNotFound, userEmail)));
                    return userResponse;
                }

                var passwordMatch = _hasherService.VerifyKey(credentials.Password, userAccount.Password);

                if (!passwordMatch)
                {
                    notificationHandler
                        .AddNotification(new Notification("InvalidCredentials", string.Format(Resources.InvalidCredentials, userEmail)));
                    return userResponse;
                }

                userResponse = _mapper.Map<UserResponse>(userAccount);

                userResponse.Token = _tokenService.GenerateToken(userResponse);
            }
            return userResponse;
        }

        public async Task<UserResponse> CreateAccount(UserCredentials credentials)
        {
            GuardClauses.IsNotNull(credentials, nameof(credentials));

            var userResponse = new UserResponse();

            if (IsValid(credentials, _userValidator))
            {
                var userAccount = _mapper.Map<User>(credentials);

                var userAlreadyExists = await _repository.CheckIfUserAlreadyExistis(userAccount);

                if (userAlreadyExists)
                {
                    notificationHandler
                        .AddNotification(new Notification("BusinessRule", string.Format(Resources.UserAccountAlreadyExists, credentials.Email)));
                    return userResponse;
                }

                userAccount.Id = Guid.NewGuid();

                userAccount.Email = userAccount.Email.ToLower();

                userAccount.Password = _hasherService.EncriptKey(userAccount.Password);

                await _repository.CreateAccount(userAccount);

                userResponse = new UserResponse { Email = credentials.Email };

                userResponse.Token = _tokenService.GenerateToken(userResponse);
            }
            return userResponse;
        }
    }
}
