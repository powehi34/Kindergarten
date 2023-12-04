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
    public class KindergartenTeacherService : IKindergartenTeacherService
    {
        private KindergartenContext _context;
        private IMapper _mapper;
        private KindergartenTeacherValidator _validator;

        public KindergartenTeacherService(KindergartenContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _validator = new KindergartenTeacherValidator();
        }

        public int Count()
        {
            return _context.Teachers.Count();
        }

        public KindergartenTeacherDTO? Create(KindergartenTeacherForCreationDTO entity)
        {
            var teacher = _mapper.Map<KindergartenTeacher>(entity);
            var result = _validator.Validate(teacher);
            if (!result.IsValid)
                return null;

            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return _mapper.Map<KindergartenTeacherDTO>(teacher);
        }

        public KindergartenTeacherDTO? Delete(int id)
        {
            var teacher = GetById(id, false);
            if (teacher == null)
                return null;

            _context.Teachers.Remove(teacher);
            _context.SaveChanges();
            return _mapper.Map<KindergartenTeacherDTO>(teacher);
        }

        public KindergartenTeacherDTO? Get(int id, bool trackChanges)
        {
            return _mapper.Map<KindergartenTeacherDTO>(GetById(id, trackChanges));
        }

        public IEnumerable<KindergartenTeacherDTO>? GetAll(bool trackChanges)
        {
            var teachers = _context.Teachers.Include(t => t.Groups);
            return _mapper.Map<IEnumerable<KindergartenTeacherDTO>>(trackChanges ? teachers.AsNoTracking() : teachers);
        }

        public IEnumerable<KindergartenTeacherDTO> GetPage(PaginationModel paginationModel, TeacherFilterModel filterModel, string sortType)
        {
            IQueryable<KindergartenTeacher>? teachers = _context.Teachers.AsNoTracking();

            teachers = TeacherFilter(filterModel, teachers);
            teachers = TeacherSort(sortType, teachers);
            teachers = TeacherPagination(paginationModel, teachers);
            return _mapper.Map<IEnumerable<KindergartenTeacherDTO>>(teachers);
        }

        private IQueryable<KindergartenTeacher> TeacherFilter(TeacherFilterModel filterModel, IQueryable<KindergartenTeacher> teachers)
        {
            teachers = teachers.Where(c => c.FullName.Contains(filterModel.Name))
                               .Where(c => c.Address.Contains(filterModel.Address))
                               .Where(c => c.PhoneNumber.Contains(filterModel.PhoneNumber));

            return teachers;
        }

        private IQueryable<KindergartenTeacher> TeacherSort(string sortType, IQueryable<KindergartenTeacher> teachers)
        {
            switch (sortType)
            {
                case "fullname_asc":
                    teachers = teachers.OrderBy(t => t.FullName);
                    break;
                case "fullname_desc":
                    teachers = teachers.OrderByDescending(t => t.FullName);
                    break;
                case "address_asc":
                    teachers = teachers.OrderBy(t => t.Address);
                    break;
                case "address_desc":
                    teachers = teachers.OrderByDescending(t => t.Address);
                    break;
                case "phonenumber_asc":
                    teachers = teachers.OrderBy(t => t.PhoneNumber);
                    break;
                case "phonenumber_desc":
                    teachers = teachers.OrderByDescending(t => t.PhoneNumber);
                    break;
                default:
                    teachers.OrderBy(c => c.FullName);
                    break;
            }

            return teachers;
        }

        private IQueryable<KindergartenTeacher> TeacherPagination(PaginationModel paginationModel, IQueryable<KindergartenTeacher> teachers)
        {
            int totalPages = (int)Math.Ceiling(teachers.Count() / (double)paginationModel.PageSize);
            if (paginationModel.PageNumber > totalPages)
                paginationModel.PageNumber = 1;
            return teachers.Skip((paginationModel.PageNumber - 1) * paginationModel.PageSize).Take(paginationModel.PageSize);
        }

        public KindergartenTeacherDTO? Update(KindergartenTeacherForUpdateDTO entity)
        {
            var teacher = GetById(entity.Id, true);
            if (teacher == null)
                return null;

            _mapper.Map(entity, teacher);
            _context.Entry(teacher).State = EntityState.Modified;
            _context.SaveChanges();
            return _mapper.Map<KindergartenTeacherDTO>(teacher);
        }

        private KindergartenTeacher? GetById(int id, bool trackChanges)
        {
            var teachers = _context.Teachers.Include(t => t.Groups).Where(t => t.Id == id);
            return trackChanges ? teachers.AsNoTracking().FirstOrDefault() : teachers.FirstOrDefault();
        }
    }
}
