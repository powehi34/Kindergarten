namespace Entities.DataTransferObjects
{
    public class GroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreationYear { get; set; }

        public GroupTypeDTO GroupType { get; set; }

        public KindergartenTeacherDTO KindergartenTeacher { get; set; }

        public List<ChildDTO> Members { get; set; } = new List<ChildDTO>();
    }
}
