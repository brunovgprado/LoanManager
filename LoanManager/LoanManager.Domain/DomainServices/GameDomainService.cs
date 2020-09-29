using LoadManager.Domain.Interfaces;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using LoanManager.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Domain.DomainServices
{
    public class GameDomainService : IGameDomainService
    {
        private readonly IUnitOfWork _unityOfWork;

        public GameDomainService(IUnitOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task CreateAsync(Game entity)
        {
           await _unityOfWork.Games.CreateAsync(entity);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> ReadAllAsync(int offset, int limit)
        {
            throw new NotImplementedException();
        }

        public Task<Game> ReadAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Game entity)
        {
            throw new NotImplementedException();
        }
    }
}
