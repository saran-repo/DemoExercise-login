using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoExercise.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession session;
        DataAccess.DAL _da;

        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            this.session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public JsonResult CheckLogin(string username, string password)
        {
            _da = new DataAccess.DAL();
            bool isSuccess = _da.CheckIfUserExists(username, password);
            
            if(isSuccess)
                this.session.SetString("UserName", username);
            
            return Json(new { success = isSuccess });
        }

        public IActionResult Logout()
        {
            this.session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
