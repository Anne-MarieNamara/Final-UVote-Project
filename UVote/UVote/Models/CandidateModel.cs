using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UVote.Models
{
    public class CandidateModel
    {
        [Display(Name = "Candidate ID")]
        [RegularExpression(@"^(\d{8})$", ErrorMessage = "Invalid id")]
        [Required(ErrorMessage = "Id required")]
        public string CandidateId { get; set; }

        [Display(Name = "First name")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Invalid name")]
        [Required(ErrorMessage = "Name required")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Invalid surname")]
        [Required(ErrorMessage = "Surname required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Manifesto required")]
        public string Manifesto { get; set; }
       
        public string ImageUrl { get; set; }

        [Display(Name = "Previous history")]
        [Required(ErrorMessage = "History required")]
        public string PreviousHistory { get; set; }

        public string CampaignId { get; set; }
        public IEnumerable<SelectListItem> CampaignIds { get; set; }

        public string EmployeeId { get; set; }
    }
}