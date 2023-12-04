using Entities.DataTransferObjects;
using Entities.FilterModels;
using Entities.Pagination;

namespace Kindergarten.Models.TeacherModels
{
    public class TeacherIndexViewModel
    {
        public string SortType { get; set; }
        public PaginationModel PaginationModel { get; set; }
        public TeacherFilterModel FilterModel { get; set; }
        public IEnumerable<KindergartenTeacherDTO> Teachers { get; set; }
    }
}
