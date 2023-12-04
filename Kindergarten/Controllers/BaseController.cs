using Contracts.Managers;
using Entities.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Kindergarten.Controllers
{
    public class BaseController : Controller
    {
        protected IServiceManager _serviceManager;

        public BaseController(IServiceManager serviceManager) => _serviceManager = serviceManager;

        protected string GetStringFromSession(HttpContext context, string key, string queryName, string defaultValue = "")
        {
            if (context.Request.Query[queryName].Count() > 0)
            {
                return context.Request.Query[queryName][0];
            }
            else if (context.Session.GetString(key) != null)
            {
                return context.Session.GetString(key);
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
