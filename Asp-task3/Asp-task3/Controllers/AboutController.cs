using Microsoft.AspNetCore.Mvc;

namespace Asp_task3.Controllers;

public class AboutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
