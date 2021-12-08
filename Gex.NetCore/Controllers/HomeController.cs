using Microsoft.AspNetCore.Mvc;

namespace Gex.Controllers;

[Route("/")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return Redirect("swagger");
    }
}
