using System.Web.Mvc;
using UVote.Models;

namespace UVote.Controllers
{
    public class StudentController : Controller
    {
        // DAO instance
        DAO dao = new DAO();

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(StudentModel studentModel)
        {
            int count = 0;
            if (ModelState.IsValid)
            {
                count = dao.Insert(studentModel);
                if (count == 1)
                {
                    ViewBag.Message = "New student added!";
                    return View("Success");
                }
                else
                {
                    ViewBag.Message = $"Error {dao.message}!";
                    return View("Error");
                }           
            }
            return View(studentModel);
        }
    }
}
