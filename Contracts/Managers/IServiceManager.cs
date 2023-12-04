using Contracts.Services;

namespace Contracts.Managers
{
    public interface IServiceManager
    {
        IChildService Child { get; }
        IGroupService Group { get; }
        IGroupTypeService GroupType { get; }
        IKindergartenTeacherService KindergartenTeacher { get; }
    }
}
