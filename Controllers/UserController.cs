using Microsoft.AspNetCore.Mvc;
using Ahmadside.Models;

namespace Ahmadside.Controllers;

public class UserController : Controller 
{

    private readonly UserDbContext _userDbCon;

    public UserController(UserDbContext userDbCon) {
        _userDbCon = userDbCon;
    }
    
    public IActionResult List() {
        List<User> users = _userDbCon.Users.ToList();
        return View(users);
    }
}