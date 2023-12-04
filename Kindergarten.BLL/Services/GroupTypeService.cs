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
    public class GroupTypeService : IGroupTypeService
    {
        private KindergartenContext _context;
        private GroupTypeValidator _validator;
        private IMapper _mapper;
        public GroupTypeService(KindergartenContext context, IMapper mapper)
        {
            _context = context;
            _validator = new GroupTypeValidator();
            _mapper = mapper;
        }

        public int Count()
        {
            return _context.GroupTypes.Count();
        }

        public GroupTypeDTO? Create(GroupTypeForCreationDTO entity)
        {
            GroupType groupType = _mapper.Map<GroupType>(entity);
            var result = _validator.Validate(groupType);
            if (!result.IsValid)
                return null;

            _context.GroupTypes.Add(groupType);
            _context.SaveChanges();
            return _mapper.Map<GroupTypeDTO?>(groupType);
        }

        public GroupTypeDTO? Delete(int id)
        {
            var groupType = GetById(id, false);
            if (groupType == null)
                return null;

            var groupTypeDTO = _mapper.Map<GroupTypeDTO>(groupType);
            _context.GroupTypes.Remove(groupType);
            _context.SaveChanges();

            return groupTypeDTO;
        }

        public GroupTypeDTO? Get(int id, bool trackChanges)
        {
            var groupType = GetById(id, trackChanges);
            return _mapper.Map<GroupTypeDTO>(groupType);
        }

        public IEnumerable<GroupTypeDTO>? GetAll(bool trackChanges)
        {
            var groupTypes = trackChanges ? _context.GroupTypes.AsNoTracking() : _context.GroupTypes;
            return _mapper.Map<IEnumerable<GroupTypeDTO>>(groupTypes);
        }

        public IEnumerable<GroupTypeDTO> GetPage(PaginationModel paginationModel, GroupTypeFilterModel filterModel, string sortType)
        {
            IQueryable<GroupType>? groupTypes = _context.GroupTypes.AsNoTracking();

            groupTypes = GroupTypeFilter(filterModel, groupTypes);
            groupTypes = GroupTypeSort(sortType, groupTypes);
            groupTypes = GroupTypePagination(paginationModel, groupTypes);
            return _mapper.Map<IEnumerable<GroupTypeDTO>>(groupTypes);
        }

        private IQueryable<GroupType> GroupTypeFilter(GroupTypeFilterModel filterModel, IQueryable<GroupType> groupTypes)
        {
            return groupTypes.Where(g => g.Name.Contains(filterModel.Name)); ;
        }

        private IQueryable<GroupType> GroupTypeSort(string sortType, IQueryable<GroupType> groups)
        {
            switch (sortType)
            {
                case "name_asc":
                    groups = groups.OrderBy(g => g.Name);
                    break;
                case "name_desc":
                    groups = groups.OrderByDescending(g => g.Name);
                    break;
                default:
                    groups.OrderBy(g => g.Name);
                    break;
            }

            return groups;
        }

        private IQueryable<GroupType> GroupTypePagination(PaginationModel paginationModel, IQueryable<GroupType> groupTypes)
        {
            int totalPages = (int)Math.Ceiling(groupTypes.Count() / (double)paginationModel.PageSize);
            if (paginationModel.PageNumber > totalPages)
                paginationModel.PageNumber = 1;
            return groupTypes.Skip((paginationModel.PageNumber - 1) * paginationModel.PageSize).Take(paginationModel.PageSize);
        }

        public GroupTypeDTO? Update(GroupTypeForUpdateDTO entity)
        {
            var group = GetById(entity.Id, true);
            if (group == null)
                return null;

            _mapper.Map(entity, group);
            _context.Entry(group).State = EntityState.Modified;
            _context.SaveChanges();
            return _mapper.Map<GroupTypeDTO>(group);
        }

        private GroupType? GetById(int id, bool trackChanges)
        {
            var groupTypes = trackChanges ? _context.GroupTypes.Where(g => g.Id == id).AsNoTracking() : _context.GroupTypes.Where(g => g.Id == id);
            return groupTypes.FirstOrDefault();
        }
    }
}
