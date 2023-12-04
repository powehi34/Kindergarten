namespace Entities.DataTransferObjects.ForCreation
{
    public class GroupForCreationDTO
    {
        public string Name { get; set; }
        public int CreationYear { get; set; }

        public int GroupTypeId { get; set; }

        public int KindergartenTeacherId { get; set; }
    }
}
