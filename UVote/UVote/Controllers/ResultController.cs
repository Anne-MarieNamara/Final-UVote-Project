using System.Collections.Generic;
using System.Web.Mvc;
using UVote.Models;

namespace UVote.Controllers
{
    public class ResultController : Controller
    {
        DAO dao = new DAO();

        // GET: Result
        public ActionResult Index()
        {
            List<RunningAndEndedElection> list = dao.GetAllRunningAndEndedElections();
            return View(list);
        }

        // Get form details
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            int campaignId = int.Parse(form["CampaignId"]);         
            List<Result> list = dao.GetResults(campaignId);
            string title = dao.GetRoleTitle(campaignId);
            string officeTerm = dao.GetOfficeTerm(campaignId);
            ViewBag.RoleTitle = title;
            ViewBag.OfficeTerm = officeTerm;
            return View("Details", list);
        }

        // Display campaign results
        public ActionResult Details()
        {
            return View();
        }

        // Update view with data from sql

    }
}
