using AutoMapper;
using Contracts.Managers;
using Contracts.Services;
using Kindergarten.BLL.Services;
using Kindergarten.DAL;

namespace Kindergarten.BLL.Managers
{
    public class ServiceManager : IServiceManager
    {
        private IChildService _childService;
        private IGroupService _groupService;
        private IGroupTypeService _groupTypeService;
        private IKindergartenTeacherService _kindergartenTeacherService;

        private KindergartenContext _context;
        private IMapper _mapper;

        public ServiceManager(KindergartenContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IChildService Child
        {
            get
            {
                if (_childService == null)
                    _childService = new ChildService(_context, _mapper);
                return _childService;
            }
        }

        public IGroupService Group
        {
            get
            {
                if (_groupService == null)
                    _groupService = new GroupService(_context, _mapper);

                return _groupService;
            }
        }

        public IGroupTypeService GroupType
        {
            get
            {
                if (_groupTypeService == null)
                    _groupTypeService = new GroupTypeService(_context, _mapper);

                return _groupTypeService;
            }
        }

        public IKindergartenTeacherService KindergartenTeacher
        {
            get
            {
                if (_kindergartenTeacherService == null)
                    _kindergartenTeacherService = new KindergartenTeacherService(_context, _mapper);

                return _kindergartenTeacherService;
            }
        }
    }
}
