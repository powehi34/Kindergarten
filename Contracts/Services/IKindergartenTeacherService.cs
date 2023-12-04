using Entities.DataTransferObjects;
using Entities.DataTransferObjects.ForCreation;
using Entities.DataTransferObjects.ForUpdate;
using Entities.FilterModels;
using Entities.Pagination;

namespace Contracts.Services
{
    public interface IKindergartenTeacherService
    {
        KindergartenTeacherDTO? Create(KindergartenTeacherForCreationDTO entity);
        KindergartenTeacherDTO? Delete(int id);
        IEnumerable<KindergartenTeacherDTO>? GetAll(bool trackChanges);
        KindergartenTeacherDTO? Get(int id, bool trackChanges);
        KindergartenTeacherDTO? Update(KindergartenTeacherForUpdateDTO entity);
        IEnumerable<KindergartenTeacherDTO> GetPage(PaginationModel paginationModel, TeacherFilterModel filterModel, string sortType);
        int Count();
    }
}
