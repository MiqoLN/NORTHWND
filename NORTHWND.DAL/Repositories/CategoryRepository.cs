using NORTHWND.Core.Abstractions.Repositories;
using NORTHWND.Core.Entities;

namespace NORTHWND.DAL.Repositories
{
    public class CategoryRepository:RepositoryBase<Category>,ICategoryRepository
    {
        public CategoryRepository(NORTHWNDContext context):base(context){ }
    }
}
