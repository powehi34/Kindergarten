namespace Entities.DataTransferObjects.ForCreation
{
    public class ChildForCreationDTO
    {
        public int Sex { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string MotherFullName { get; set; }
        public string FatherFullName { get; set; }

        public int GroupId { get; set; }
    }
}
