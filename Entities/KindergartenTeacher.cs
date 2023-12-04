namespace Entities
{
    public class KindergartenTeacher
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? ExperienceInfo { get; set; }
        public string? AwardsInfo { get; set; }

        public List<Group> Groups { get; set; } = new List<Group>();
    }
}
