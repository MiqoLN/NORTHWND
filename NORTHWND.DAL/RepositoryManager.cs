using NORTHWND.Core.Abstractions;
using NORTHWND.Core.Abstractions.Repositories;
using NORTHWND.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NORTHWND.DAL
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly NORTHWNDContext _dbContext;
        public RepositoryManager(NORTHWNDContext context)
        {
            _dbContext = context;
        }
        private IUserRepository _users;
        public IUserRepository Users => _users ?? (_users = new UserRepository(_dbContext));
        private IOrderRepository _orders;
        public IOrderRepository Orders => _orders ?? (_orders = new OrderRepository(_dbContext));
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
