namespace Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreationYear { get; set; }

        public int GroupTypeId { get; set; }
        public GroupType GroupType { get; set; }

        public int KindergartenTeacherId { get; set; }
        public KindergartenTeacher KindergartenTeacher { get; set; }

        public List<Child> Members { get; set; } = new List<Child>();
    }
}