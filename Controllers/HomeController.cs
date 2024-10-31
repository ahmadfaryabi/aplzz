using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Ahmadside.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Http.Connections;

namespace Ahmadside.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var uName = HttpContext.Session.GetString("username");
        var fName = HttpContext.Session.GetString("firstname");
        var aName = HttpContext.Session.GetString("aftername");
        var ppic = HttpContext.Session.GetString("profilePicture");
        ViewBag.userN = uName;
        ViewBag.fN = fName;
        ViewBag.aN = aName;
        ViewBag.pp = ppic;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }
}
