using System.Collections.Generic;
using System.Web.Mvc;
using UVote.Models;

namespace UVote.Controllers
{
    public class CampaignController : Controller
    {
        DAO dao = new DAO();

        // GET: Campaign
        public ActionResult Index()
        {
            List<Campaign> list = dao.GetCampaigns();
            
            return View(list);
        }     

        // GET: Campaign/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Campaign/Create
        [HttpPost]
        public ActionResult Create(CampaignModel campaignModel)
        {
            int count = 0;
            if (ModelState.IsValid)
            {
                count = dao.InsertCampaign(campaignModel);
                if (count == 1)
                {
                    ViewBag.Message = "Campaign created!";
                    return View("Success");
                }
                else
                {
                    ViewBag.Message = $"Error {dao.message}!";
                    return View("Error");
                }
            }
            return View(campaignModel);
        }
    }
}
