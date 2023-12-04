namespace Entities
{
    public class Child
    {
        public int Id { get; set; }
        public int Sex { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string MotherFullName { get; set; }
        public string FatherFullName { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}