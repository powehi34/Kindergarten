using Contracts.Managers;
using Entities.DataTransferObjects.ForCreation;
using Entities.DataTransferObjects.ForUpdate;
using Entities.FilterModels;
using Entities.Pagination;
using Kindergarten.Models;
using Kindergarten.Models.ChildModels;
using Kindergarten.Models.GroupModels;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten.Controllers
{
    public class GroupController : BaseController
    {
        public GroupController(IServiceManager serviceManager) : base(serviceManager)
        {
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 20, string sortType = "name_asc")
        {
            var paginationModel = new PaginationModel(_serviceManager.Group.Count(), pageNumber, pageSize);
            var filterModel = GetFilterModel();
            var groups = _serviceManager.Group.GetPage(paginationModel, filterModel, sortType);
            var viewModel = new GroupIndexViewModel()
            {
                SortType = sortType,
                Groups = groups,
                PaginationModel = paginationModel,
                FilterModel = filterModel
            };
            return View("Index", viewModel);
        }
        private GroupFilterModel GetFilterModel()
        {
            string name = GetStringFromSession(HttpContext, "group", "groupName");
            HttpContext.Session.SetString("group", name);
            string groupType = GetStringFromSession(HttpContext, "grouptype", "groupType");
            HttpContext.Session.SetString("grouptype", groupType);
            var filterModel = new GroupFilterModel()
            {
                Name = name,
                GroupType = groupType
            };

            return filterModel;
        }
        public IActionResult CreateView()
        {
            var groupViewModel = new GroupForCreationViewModel()
            {
                GroupTypes = _serviceManager.GroupType.GetAll(false),
                Teachers = _serviceManager.KindergartenTeacher.GetAll(false)
            };
            return View("Create", groupViewModel);
        }

        public IActionResult Create(GroupForCreationDTO newGroup)
        {
            _serviceManager.Group.Create(newGroup);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateView(int id)
        {
            var group = _serviceManager.Group.Get(id, true);
            var groupTypes = _serviceManager.GroupType.GetAll(false);
            var teachers = _serviceManager.KindergartenTeacher.GetAll(false);
            var updateViewModel = new GroupForUpdateViewModel(group, groupTypes, teachers);
            return View("Update", updateViewModel);
        }

        public IActionResult Update(GroupForUpdateDTO updatedGroup)
        {
            _serviceManager.Group.Update(updatedGroup);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _serviceManager.Group.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Info(int id)
        {
            return View("Info", _serviceManager.Group.Get(id, false));
        }
    }
}
