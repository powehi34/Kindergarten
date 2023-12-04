using Entities.DataTransferObjects;
using Entities.DataTransferObjects.ForCreation;
using Entities.DataTransferObjects.ForUpdate;
using Entities.FilterModels;
using Entities.Pagination;

namespace Contracts.Services
{
    public interface IGroupService
    {
        GroupDTO? Create(GroupForCreationDTO entity);
        GroupDTO? Delete(int id);
        IEnumerable<GroupDTO>? GetAll(bool trackChanges);
        GroupDTO? Get(int id, bool trackChanges);
        GroupDTO? Update(GroupForUpdateDTO entity);
        IEnumerable<GroupDTO>? GetPage(PaginationModel paginationModel, GroupFilterModel filterModel, string sortType);
        int Count();
    }
}
