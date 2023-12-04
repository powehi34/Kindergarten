using Entities.DataTransferObjects;
using Entities.DataTransferObjects.ForCreation;
using Entities.DataTransferObjects.ForUpdate;
using Entities.FilterModels;
using Entities.Pagination;

namespace Contracts.Services
{
    public interface IChildService
    {
        ChildDTO? Create(ChildForCreationDTO entity);
        ChildDTO? Delete(int id);
        IEnumerable<ChildDTO>? GetAll(bool trackChanges);
        ChildDTO? Get(int id, bool trackChanges);
        ChildDTO? Update(ChildForUpdateDTO entity);
        IEnumerable<ChildDTO>? GetPage(PaginationModel paginationModel, ChildFilterModel filterModel, string sortType);
        int Count();
    }
}
