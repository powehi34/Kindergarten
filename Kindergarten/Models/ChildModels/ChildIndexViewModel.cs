using Entities.DataTransferObjects;
using Entities.FilterModels;
using Entities.Pagination;

namespace Kindergarten.Models.ChildModels
{
    public class ChildIndexViewModel
    {
        public PaginationModel PaginationModel { get; set; }
        public ChildFilterModel FilterModel { get; set; }
        public IEnumerable<ChildDTO>? Children { get; set; }
        public string SortType { get; set; }
    }
}
