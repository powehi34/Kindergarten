using Entities.DataTransferObjects;
using Entities.DataTransferObjects.ForCreation;
using Entities.DataTransferObjects.ForUpdate;
using Entities.FilterModels;
using Entities.Pagination;

namespace Contracts.Services
{
    public interface IGroupTypeService
    {
        GroupTypeDTO? Create(GroupTypeForCreationDTO entity);
        GroupTypeDTO? Delete(int id);
        IEnumerable<GroupTypeDTO>? GetAll(bool trackChanges);
        GroupTypeDTO? Get(int id, bool trackChanges);
        GroupTypeDTO? Update(GroupTypeForUpdateDTO entity);
        IEnumerable<GroupTypeDTO> GetPage(PaginationModel paginationModel, GroupTypeFilterModel filterModel, string sortType);
        int Count();
    }
}
