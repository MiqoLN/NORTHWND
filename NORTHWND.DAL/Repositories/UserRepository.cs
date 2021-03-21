using NORTHWND.Core.Abstractions.Repositories;
using NORTHWND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(NORTHWNDContext context):base(context){}

    }
}
