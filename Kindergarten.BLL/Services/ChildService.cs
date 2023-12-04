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
    public class ChildService : IChildService
    {
        private KindergartenContext _context;
        private IMapper _mapper;
        private ChildValidator _validator;

        public ChildService(KindergartenContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _validator = new ChildValidator();
        }

        public ChildDTO? Create(ChildForCreationDTO entity)
        {
            var child = _mapper.Map<Child>(entity);
            var result = _validator.Validate(child);
            if (!result.IsValid)
                return null;

            _context.Children.Add(child);
            _context.SaveChanges();
            return _mapper.Map<ChildDTO>(child);
        }

        public ChildDTO? Delete(int id)
        {
            var child = GetById(id, false);
            if (child == null)
                return null;

            _context.Children.Remove(child);
            _context.SaveChanges();
            return _mapper.Map<ChildDTO>(child);
        }

        public ChildDTO? Get(int id, bool trackChanges)
        {
            return _mapper.Map<ChildDTO>(GetById(id, trackChanges));
        }

        public IEnumerable<ChildDTO>? GetAll(bool trackChanges)
        {
            var children = _context.Children.Include(c => c.Group);
            return _mapper.Map<IEnumerable<ChildDTO>>(trackChanges ? children.AsNoTracking() : children);
        }

        public IEnumerable<ChildDTO>? GetPage(PaginationModel paginationModel, ChildFilterModel filterModel, string sortType)
        {
            IQueryable<Child>? children = _context.Children.Include(c => c.Group).AsNoTracking();

            children = ChildFilter(filterModel, children);
            children = ChildSort(sortType, children);
            children = ChildPagination(paginationModel, children);
            return _mapper.Map<IEnumerable<ChildDTO>>(children);
        }

        public ChildDTO? Update(ChildForUpdateDTO entity)
        {
            var child = GetById(entity.Id, true);
            if (child == null)
                return null;

            _mapper.Map(entity, child);
            _context.Entry(child).State = EntityState.Modified;
            _context.SaveChanges();
            return _mapper.Map<ChildDTO>(child);
        }

        private Child? GetById(int id, bool trackChanges)
        {
            var children = _context.Children.Include(c => c.Group).Where(g => g.Id == id);
            return trackChanges ? children.AsNoTracking().FirstOrDefault() : children.FirstOrDefault();
        }

        private IQueryable<Child> ChildFilter(ChildFilterModel filterModel, IQueryable<Child> children)
        {
            children = children.Where(c => c.FullName.Contains(filterModel.FullName))
                               .Where(c => c.MotherFullName.Contains(filterModel.MotherFullName))
                               .Where(c => c.FatherFullName.Contains(filterModel.FatherFullName))
                               .Where(c => c.Group.Name.Contains(filterModel.GroupName));

            return children;
        }

        private IQueryable<Child> ChildSort(string sortType, IQueryable<Child> children)
        {
            switch (sortType)
            {
                case "fullname_asc":
                    children = children.OrderBy(c => c.FullName);
                    break;
                case "fullname_desc":
                    children = children.OrderByDescending(c => c.FullName);
                    break;
                case "motherfullname_asc":
                    children = children.OrderBy(c => c.MotherFullName);
                    break;
                case "motherfullname_desc":
                    children = children.OrderByDescending(c => c.MotherFullName);
                    break;
                case "fatherfullname_asc":
                    children = children.OrderBy(c => c.FatherFullName);
                    break;
                case "fatherfullname_desc":
                    children = children.OrderByDescending(u => u.FatherFullName);
                    break;
                case "birthdate_asc":
                    children = children.OrderBy(c => c.BirthDate);
                    break;
                case "birthdate_desc":
                    children = children.OrderByDescending(c => c.BirthDate);
                    break;
                case "sex_asc":
                    children = children.OrderBy(c => c.Sex);
                    break;
                case "sex_desc":
                    children = children.OrderByDescending(c => c.Sex);
                    break;
                case "groupname_asc":
                    children = children.OrderBy(c => c.Group.Name);
                    break;
                case "groupname_desc":
                    children = children.OrderByDescending(c => c.Group.Name);
                    break;
                default:
                    children.OrderBy(c => c.FullName);
                    break;
            }

            return children;
        }

        private IQueryable<Child> ChildPagination(PaginationModel paginationModel, IQueryable<Child> children)
        {
            int totalPages = (int)Math.Ceiling(children.Count() / (double)paginationModel.PageSize);
            if (paginationModel.PageNumber > totalPages)
                paginationModel.PageNumber = 1;
            return children.Skip((paginationModel.PageNumber - 1) * paginationModel.PageSize).Take(paginationModel.PageSize);
        }

        public int Count()
        {
            return _context.Children.Count();
        }
    }
}

