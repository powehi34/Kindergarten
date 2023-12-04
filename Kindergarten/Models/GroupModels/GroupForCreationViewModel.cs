using Entities.DataTransferObjects;

namespace Kindergarten.Models.GroupModels
{
    public class GroupForCreationViewModel
    {
        public IEnumerable<GroupTypeDTO> GroupTypes { get; set; }
        public IEnumerable<KindergartenTeacherDTO> Teachers { get; set; }
    }
}
