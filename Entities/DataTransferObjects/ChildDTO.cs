namespace Entities.DataTransferObjects
{
    public class ChildDTO
    {
        public int Id { get; set; }
        public int Sex { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string MotherFullName { get; set; }
        public string FatherFullName { get; set; }

        public GroupDTO Group { get; set; }
    }
}
