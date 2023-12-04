namespace Entities.DataTransferObjects.ForUpdate
{
    public class ChildForUpdateDTO
    {
        public int Id { get; set; }
        public int Sex { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string MotherFullName { get; set; }
        public string FatherFullName { get; set; }

        public int GroupId { get; set; }
    }
}
