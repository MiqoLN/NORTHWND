using NORTHWND.Core.Abstractions.Repositories;
using NORTHWND.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.DAL.Repositories
{
    public class CategoryRepository:RepositoryBase<Category>,ICategoryRepository
    {
        public CategoryRepository(NORTHWNDContext context):base(context){ }
    }
}
