using Entities.DataTransferObjects;
using Entities.FilterModels;
using Entities.Pagination;

namespace Kindergarten.Models.GroupModels
{
    public class GroupIndexViewModel
    {
        public string SortType { get; set; }
        public IEnumerable<GroupDTO> Groups { get; set; }
        public GroupFilterModel FilterModel { get; set; }
        public PaginationModel PaginationModel { get; set; }
    }
}
