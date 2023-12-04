using Contracts.Managers;
using Entities.DataTransferObjects.ForCreation;
using Entities.DataTransferObjects.ForUpdate;
using Entities.FilterModels;
using Entities.Pagination;
using Kindergarten.Models;
using Kindergarten.Models.ChildModels;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten.Controllers
{
    public class ChildController : BaseController
    {
        public ChildController(IServiceManager serviceManager) : base(serviceManager)
        {
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 20, string sortType = "fullname_asc")
        {
            var paginationModel = new PaginationModel(_serviceManager.Child.Count(), pageNumber, pageSize);
            var filterModel = GetFilterModel();
            var children = _serviceManager.Child.GetPage(paginationModel, filterModel, sortType);
            var viewModel = new ChildIndexViewModel()
            {
                SortType = sortType,
                Children = children,
                PaginationModel = paginationModel,
                FilterModel = filterModel
            };
            return View("Index", viewModel);
        }

        private ChildFilterModel GetFilterModel()
        {
            string fullname = GetStringFromSession(HttpContext, "childfullname", "fullName");
            HttpContext.Session.SetString("childfullname", fullname);
            string motherfullname = GetStringFromSession(HttpContext, "motherfullname", "motherFullName");
            HttpContext.Session.SetString("motherfullname", motherfullname);
            string fatherfullname = GetStringFromSession(HttpContext, "fatherfullname", "fatherFullName");
            HttpContext.Session.SetString("fatherfullname", fatherfullname);
            string groupname = GetStringFromSession(HttpContext, "groupname", "groupName");
            HttpContext.Session.SetString("groupname", fatherfullname);
            var filterModel = new ChildFilterModel()
            {
                FullName = fullname,
                MotherFullName = motherfullname,
                FatherFullName = fatherfullname,
                GroupName = groupname
            };

            return filterModel;
        }

        public IActionResult CreateView()
        {
            return View("Create", _serviceManager.Group.GetAll(false));
        }

        public IActionResult Create(ChildForCreationDTO newChild)
        {
            _serviceManager.Child.Create(newChild);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateView(int id)
        {
            var updateViewModel = new ChildForUpdateViewModel()
            {
                Child = _serviceManager.Child.Get(id, true),
                Groups = _serviceManager.Group.GetAll(false)
            };
            return View("Update", updateViewModel);
        }

        public IActionResult Update(ChildForUpdateDTO updatedChild)
        {
            _serviceManager.Child.Update(updatedChild);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _serviceManager.Child.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Info(int id)
        {
            return View("Info", _serviceManager.Child.Get(id, false));
        }
    }
}
