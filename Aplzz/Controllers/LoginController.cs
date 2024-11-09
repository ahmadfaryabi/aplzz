using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
<<<<<<< HEAD
using Aplzz.Models;
=======
using Ahmadside.Models;
>>>>>>> 5847ef8 (logg inn funksjoon)
=======
using Aplzz.Models;
>>>>>>> 5504f1b (database endringer)
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.AspNetCore.Http.Connections;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> ab71774 (fikset sql lite feil. :))
using Aplzz.DAL;
namespace Aplzz.Controllers;

public class LoginController : Controller 
{
<<<<<<< HEAD
<<<<<<< HEAD
  private readonly PostDbContext _userDB;

  public LoginController(PostDbContext userDb) 
=======
namespace Ahmadside.Controllers;
=======
namespace Aplzz.Controllers;
>>>>>>> 5504f1b (database endringer)

public class LoginController : Controller 
{
  private readonly UserDbContext _userDB;

  public LoginController(UserDbContext userDb) 
>>>>>>> 5847ef8 (logg inn funksjoon)
=======
  private readonly DbContexts _userDB;

  public LoginController(DbContexts userDb) 
>>>>>>> 86d362f (login system endring)
=======
  private readonly PostDbContext _userDB;

  public LoginController(PostDbContext userDb) 
>>>>>>> ab71774 (fikset sql lite feil. :))
  {
    _userDB = userDb;
  }

  public IActionResult Index() 
  {
    if(HttpContext.Session.GetString("username") != null) {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
      return RedirectToAction("Index", "Post");
=======
      return RedirectToAction("Index", "Home");
>>>>>>> 5847ef8 (logg inn funksjoon)
=======
      return RedirectToAction("Post", "Index");
>>>>>>> 5504f1b (database endringer)
=======
      return RedirectToAction("Index", "Post");
>>>>>>> d202e60 (endring på stil)
    } else {
      return View();
    }
  }

 [HttpPost]
 public IActionResult Index(LoginModel loginModel) {
  if(ModelState.IsValid) {
    if(CheckEmail(loginModel.Email) == true && CheckPassword(loginModel.Password) == true) {
      // linq query:
      var MyUser = from user in _userDB.Users 
      where user.Email == loginModel.Email && user.Password == loginModel.Password
      select user;
      // create session:
      foreach(var res in MyUser) {
        HttpContext.Session.SetString("id", res.IdUser.ToString());
        HttpContext.Session.SetString("username", res.Username.ToString());
        HttpContext.Session.SetString("firstname", res.Firstname.ToString());
        HttpContext.Session.SetString("aftername", res.Aftername.ToString());
        HttpContext.Session.SetString("email", res.Email.ToString());
<<<<<<< HEAD
       // HttpContext.Session.SetString("profilePicture", res.ProfilePicture.ToString());
      }
      return RedirectToAction("Index", "Post");
=======
        HttpContext.Session.SetString("profilePicture", res.ProfilePicture.ToString());
      }
<<<<<<< HEAD
      return RedirectToAction("Index", "Home");
>>>>>>> 5847ef8 (logg inn funksjoon)
=======
      return RedirectToAction("Index", "Post");
>>>>>>> 86d362f (login system endring)
    } else {
      TempData["ErrorMessage"] = "E-mail or password is incorrect or account does not exist!";
    }
  }
  return View();
 } 



  public IActionResult Register() 
  {
    return View();
  }


  [HttpPost]
  public IActionResult Register(User userModel) {

    if(ModelState.IsValid) {
      // used to count errors
      User userr = new User {
        Firstname = userModel.Firstname,
        Aftername = userModel.Aftername,
        Email = userModel.Email,
        Password = userModel.Password,
        Username = userModel.Username,
        Date_Started = DateTime.Now,
        ProfilePicture = userModel.ProfilePicture
      };
      bool checkName = CheckUserName(userr.Username);
      bool checkEmail = CheckEmail(userr.Email);

      // check username exist
      if(checkName == true) {
        TempData["ErrorUserName"] = "Username exist already choose another one";
      }

      if(userr.ProfilePicture == null) {
        userr.ProfilePicture = "/images/profile.jpg";
      }

      if(checkEmail == true) {
        TempData["ErrorEmail"] = "Email exist already choose another one";
      }
      if(checkName == false && checkEmail == false) {
        _userDB.Users.Add(userr);
        _userDB.SaveChanges();
        TempData["SuccessMsg"] = "Account successfully added. Login now!";
      }
    }
    return View();
  }

  public bool CheckUserName(string Username) {
    return _userDB.Users.Any(a => a.Username == Username);
  }

  public bool CheckEmail(string Email) {
    return _userDB.Users.Any(a => a.Email == Email);
  }
  public bool CheckPassword(string Password) {
    return _userDB.Users.Any(a => a.Password == Password);
  }

  public IActionResult Logout() {
    HttpContext.Session.Clear();
    return RedirectToAction("Index", "Login");
  }
<<<<<<< HEAD
}
=======

<<<<<<< HEAD
  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
>>>>>>> 5847ef8 (logg inn funksjoon)
=======
//   [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//     public IActionResult Error()
//     {
//         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//     }
}
>>>>>>> 2b5b12d (fiksing)
