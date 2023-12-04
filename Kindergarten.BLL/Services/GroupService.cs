using AutoMapper;
using Contracts.Services;
using Entities;
using Entities.DataTransferObjects;
using Entities.DataTransferObjects.ForCreation;
using Entities.DataTransferObjects.ForUpdate;
using Entities.FilterModels;
using Entities.Pagination;
using Kindergarten.BLL.Validation;
using Kindergarten.DAL;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.BLL.Services
{
    public class GroupService : IGroupService
    {
        private KindergartenContext _context;
        private IMapper _mapper;
        private GroupValidator _validator;

        public GroupService(KindergartenContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _validator = new GroupValidator();
        }

        public int Count()
        {
            return _context.Groups.Count();
        }

        public GroupDTO? Create(GroupForCreationDTO entity)
        {
            entity.CreationYear = DateTime.Now.Year;
            var group = _mapper.Map<Group>(entity);
            var result = _validator.Validate(group);
            if (!result.IsValid)
                return null;

            _context.Groups.Add(group);
            _context.SaveChanges();
            return _mapper.Map<GroupDTO>(group);
        }

        public GroupDTO? Delete(int id)
        {
            var group = GetById(id, false);
            if (group == null)
                return null;

            _context.Groups.Remove(group);
            _context.SaveChanges();
            return _mapper.Map<GroupDTO>(group);
        }

        public GroupDTO? Get(int id, bool trackChanges)
        {
            var group = GetById(id, trackChanges);
            return _mapper.Map<GroupDTO>(group);
        }

        public IEnumerable<GroupDTO>? GetAll(bool trackChanges)
        {
            var groups = _context.Groups.Include(g => g.GroupType).Include(g => g.KindergartenTeacher);
            return trackChanges ? _mapper.Map<IEnumerable<GroupDTO>>(groups.AsNoTracking()) : _mapper.Map<IEnumerable<GroupDTO>>(groups);
        }

        public IEnumerable<GroupDTO>? GetPage(PaginationModel paginationModel, GroupFilterModel filterModel, string sortType)
        {
            IQueryable<Group>? groups = _context.Groups.Include(g => g.GroupType).Include(g => g.KindergartenTeacher).AsNoTracking();

            groups = GroupFilter(filterModel, groups);
            groups = GroupSort(sortType, groups);
            groups = GroupPagination(paginationModel, groups);
            return _mapper.Map<IEnumerable<GroupDTO>>(groups);
        }

        private IQueryable<Group> GroupFilter(GroupFilterModel filterModel, IQueryable<Group> groups)
        {
            groups = groups.Where(g => g.Name.Contains(filterModel.Name))
                           .Where(g => g.GroupType.Name.Contains(filterModel.GroupType));

            return groups;
        }

        private IQueryable<Group> GroupSort(string sortType, IQueryable<Group> groups)
        {
            switch (sortType)
            {
                case "name_asc":
                    groups = groups.OrderBy(g => g.Name);
                    break;
                case "name_desc":
                    groups = groups.OrderByDescending(g => g.Name);
                    break;
                case "grouptype_asc":
                    groups = groups.OrderBy(g => g.GroupType.Name);
                    break;
                case "grouptype_desc":
                    groups = groups.OrderByDescending(g => g.GroupType.Name);
                    break;
                case "creationyear_asc":
                    groups = groups.OrderBy(g => g.CreationYear);
                    break;
                case "creationyear_desc":
                    groups = groups.OrderByDescending(g => g.CreationYear);
                    break;
                case "teachername_asc":
                    groups = groups.OrderBy(g => g.KindergartenTeacher.FullName);
                    break;
                case "teachername_desc":
                    groups = groups.OrderByDescending(g => g.KindergartenTeacher.FullName);
                    break;
                default:
                    groups.OrderBy(g => g.Name);
                    break;
            }

            return groups;
        }

        private IQueryable<Group> GroupPagination(PaginationModel paginationModel, IQueryable<Group> groups)
        {
            int totalPages = (int)Math.Ceiling(groups.Count() / (double)paginationModel.PageSize);
            if (paginationModel.PageNumber > totalPages)
                paginationModel.PageNumber = 1;
            return groups.Skip((paginationModel.PageNumber - 1) * paginationModel.PageSize).Take(paginationModel.PageSize);
        }

        public GroupDTO? Update(GroupForUpdateDTO entity)
        {
            var group = GetById(entity.Id, true);
            if (group == null)
                return null;

            _mapper.Map(entity, group);
            _context.Entry(group).State = EntityState.Modified;
            _context.SaveChanges();

            return _mapper.Map<GroupDTO>(group);
        }

        private Group? GetById(int id, bool trackChanges)
        {
            var groups = _context.Groups.Include(g => g.GroupType).Include(g => g.KindergartenTeacher).Include(g => g.Members).Where(g => g.Id == id);
            return trackChanges ? groups.AsNoTracking().FirstOrDefault() : groups.FirstOrDefault();
        }
    }
}
