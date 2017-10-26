using System.Web.Mvc;
using UVote.Models;

namespace UVote.Controllers
{
    public class AdminController : Controller
    {
        DAO dao = new DAO();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(AdminModel admin)
        {
            ModelState.Remove("EmployeeId");
            ModelState.Remove("Name");

            if (ModelState.IsValid)
            {
                admin.EmployeeId = dao.AdminLogin(admin);
                if (admin.EmployeeId != null)
                {
                    Session["employeeId"] = admin.EmployeeId;
                    ViewBag.Session = Session["employeeId"];
                    //return View("Index");
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = $"Error {dao.message}!";
                    return View("Error");
                }
            }
            else return View(admin);
        }

        // Create admin
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AdminModel adminModel)
        {
            int count = 0;
            if (ModelState.IsValid)
            {
                count = dao.InsertAdmin(adminModel);
                if (count == 1)
                {
                    ViewBag.Message = "Admin created!";
                    return View("Success");
                }
                else
                {
                    ViewBag.Message = $"Error {dao.message}!";
                    return View("Error");
                }   
            }
            return View(adminModel);
        }

        // Logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return View("../Home/Index");
        }
    }
}