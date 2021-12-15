using Microsoft.AspNetCore.Mvc;

namespace Gex.Controllers;

[Route("/")]
public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return Redirect("swagger");
    }
}
