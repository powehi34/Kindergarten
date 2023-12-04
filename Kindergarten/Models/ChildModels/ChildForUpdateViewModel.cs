using Entities.DataTransferObjects;

namespace Kindergarten.Models.ChildModels
{
    public class ChildForUpdateViewModel
    {
        public ChildDTO Child { get; set; }
        public IEnumerable<GroupDTO> Groups { get; set; }
    }
}
