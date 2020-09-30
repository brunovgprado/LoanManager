using LoadManager.Domain.Interfaces;
using LoanManager.Domain.Entities;
using LoanManager.Domain.Interfaces.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoanManager.Domain.DomainServices
{
    public class FriendDomainService : IFriendDomainService
    {
        private readonly IUnitOfWork _unityOfWork;

        public FriendDomainService(IUnitOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        public async Task<Guid> CreateAsync(Friend entity)
        {
            entity.Id = Guid.NewGuid();
            await _unityOfWork.Friends.CreateAsync(entity);
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unityOfWork.Games.DeleteAsync(id);
        }

        public async Task<IEnumerable<Friend>> ReadAllAsync(int offset, int limit)
        {
            return await _unityOfWork.Friends.ReadAllAsync(offset, limit);
        }

        public async Task<Friend> ReadAsync(Guid id)
        {
            return await _unityOfWork.Friends.ReadAsync(id);

        }

        public void Update(Friend entity)
        {
            _unityOfWork.Friends.Update(entity);
        }
    }
}
