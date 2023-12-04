using Entities.DataTransferObjects;
using Entities.FilterModels;
using Entities.Pagination;

namespace Kindergarten.Models.GroupTypeModels
{
    public class GroupTypeIndexViewModel
    {
        public PaginationModel PaginationModel { get; set; }
        public string SortType { get; set; }
        public GroupTypeFilterModel FilterModel { get; set; }
        public IEnumerable<GroupTypeDTO> GroupTypes { get; set; }
    }
}
