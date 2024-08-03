using Microsoft.AspNetCore.Mvc;

namespace FinanceManagementSystem.WebUI.ViewComponents.LayoutViewComponents
{
    public class _ScriptsLayoutComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
