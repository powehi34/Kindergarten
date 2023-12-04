using Contracts.Managers;
using Entities.DataTransferObjects.ForCreation;
using Entities.DataTransferObjects.ForUpdate;
using Entities.FilterModels;
using Entities.Pagination;
using Kindergarten.Models.GroupModels;
using Kindergarten.Models.TeacherModels;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten.Controllers
{
    public class TeacherController : BaseController
    {
        public TeacherController(IServiceManager serviceManager) : base(serviceManager)
        {
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 20, string sortType = "fullname_asc")
        {
            var paginationModel = new PaginationModel(_serviceManager.KindergartenTeacher.Count(), pageNumber, pageSize);
            var filterModel = GetFilterModel();
            var teachers = _serviceManager.KindergartenTeacher.GetPage(paginationModel, filterModel, sortType);
            var viewModel = new TeacherIndexViewModel()
            {
                SortType = sortType,
                Teachers = teachers,
                PaginationModel = paginationModel,
                FilterModel = filterModel
            };
            return View("Index", viewModel);
        }

        private TeacherFilterModel GetFilterModel()
        {
            string name = GetStringFromSession(HttpContext, "teachername", "teacherName");
            HttpContext.Session.SetString("teachername", name);
            string address = GetStringFromSession(HttpContext, "address", "address");
            HttpContext.Session.SetString("grouptype", address);
            string phonenumber = GetStringFromSession(HttpContext, "phonenumber", "phoneNumber");
            HttpContext.Session.SetString("phonenumber", phonenumber);
            var filterModel = new TeacherFilterModel()
            {
                Name = name,
                Address = address,
                PhoneNumber = phonenumber
            };

            return filterModel;
        }

        public IActionResult CreateView()
        {
            return View("Create");
        }

        public IActionResult Create(KindergartenTeacherForCreationDTO newTeacher)
        {
            _serviceManager.KindergartenTeacher.Create(newTeacher);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateView(int id)
        {
            var teacher = _serviceManager.KindergartenTeacher.Get(id, true); ;
            return View("Update", teacher);
        }

        public IActionResult Update(KindergartenTeacherForUpdateDTO updatedTeacher)
        {
            _serviceManager.KindergartenTeacher.Update(updatedTeacher);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _serviceManager.KindergartenTeacher.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Info(int id)
        {
            return View("Info", _serviceManager.KindergartenTeacher.Get(id, false));
        }
    }
}
