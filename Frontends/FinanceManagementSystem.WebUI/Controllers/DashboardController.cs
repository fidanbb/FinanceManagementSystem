using Microsoft.AspNetCore.Mvc;

namespace FinanceManagementSystem.WebUI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
