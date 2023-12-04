namespace Entities.DataTransferObjects.ForUpdate
{
    public class GroupForUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreationYear { get; set; }

        public int GroupTypeId { get; set; }

        public int KindergartenTeacherId { get; set; }
    }
}
