using Contracts.Managers;
using Entities.DataTransferObjects.ForCreation;
using Entities.DataTransferObjects.ForUpdate;
using Entities.FilterModels;
using Entities.Pagination;
using Kindergarten.Models.GroupModels;
using Kindergarten.Models.GroupTypeModels;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten.Controllers
{
    public class GroupTypeController : BaseController
    {
        public GroupTypeController(IServiceManager serviceManager) : base(serviceManager)
        {
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 20, string sortType = "name_asc")
        {
            var paginationModel = new PaginationModel(_serviceManager.GroupType.Count(), pageNumber, pageSize);
            var filterModel = GetFilterModel();
            var groupTypes = _serviceManager.GroupType.GetPage(paginationModel, filterModel, sortType);
            var viewModel = new GroupTypeIndexViewModel()
            {
                SortType = sortType,
                GroupTypes = groupTypes,
                PaginationModel = paginationModel,
                FilterModel = filterModel
            };
            return View("Index", viewModel);
        }

        private GroupTypeFilterModel GetFilterModel()
        {
            string groupType = GetStringFromSession(HttpContext, "grouptypename", "groupType");
            HttpContext.Session.SetString("grouptypename", groupType);
            var filterModel = new GroupTypeFilterModel()
            {
                Name = groupType
            };

            return filterModel;
        }
        public IActionResult CreateView()
        {
            return View("Create");
        }

        public IActionResult Create(GroupTypeForCreationDTO newGroupType)
        {
            _serviceManager.GroupType.Create(newGroupType);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateView(int id)
        {
            return View("Update", _serviceManager.GroupType.Get(id, true));
        }

        public IActionResult Update(GroupTypeForUpdateDTO updatedGroupType)
        {
            _serviceManager.GroupType.Update(updatedGroupType);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _serviceManager.GroupType.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
