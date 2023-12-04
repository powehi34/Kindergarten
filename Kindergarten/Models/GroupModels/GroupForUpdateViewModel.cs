using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kindergarten.Models.GroupModels
{
    public class GroupForUpdateViewModel
    {
        public SelectList GroupTypeSelectList { get; set; }
        public SelectList TeacherSelectList { get; set; }
        public GroupDTO Group { get; set; }
        public GroupForUpdateViewModel(GroupDTO group, IEnumerable<GroupTypeDTO> groupTypes, IEnumerable<KindergartenTeacherDTO> teachers)
        {
            Group = group;
            GroupTypeSelectList = new SelectList(groupTypes, "Id", "Name", group.GroupType.Id);
            TeacherSelectList = new SelectList(teachers, "Id", "FullName", group.KindergartenTeacher.Id);
        }
    }
}
