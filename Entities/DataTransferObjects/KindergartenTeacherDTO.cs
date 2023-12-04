namespace Entities.DataTransferObjects
{
    public class KindergartenTeacherDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string? ExperienceInfo { get; set; }
        public string? AwardsInfo { get; set; }

        public List<GroupDTO> Groups { get; set; } = new List<GroupDTO>();
    }
}
