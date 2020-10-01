using LoanManager.Domain.Interfaces;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using System;
using System.Collections.Generic;
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

        #region CRUD operations
        public async Task<Guid> CreateAsync(Game entity)
        {
            entity.Id = Guid.NewGuid();
            await _unityOfWork.Games.CreateAsync(entity);
            return entity.Id;
        }

        public async Task<IEnumerable<Game>> ReadAllAsync(int offset, int limit)
        {
            return await _unityOfWork.Games.ReadAllAsync(offset, limit);
        }

        public async Task<Game> ReadAsync(Guid id)
        {
            return await _unityOfWork.Games.ReadAsync(id);
        }

        public void Update(Game entity)
        {
             _unityOfWork.Games.Update(entity);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _unityOfWork.Games.DeleteAsync(id);            
        }
        #endregion
    }
}
