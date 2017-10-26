using System.ComponentModel.DataAnnotations;

namespace UVote.Models
{
    public class CampaignModel
    {
        [Display(Name = "Role Title")]
        [Required(ErrorMessage = "Role required")]
        public string RoleTitle { get; set; }

        [Display(Name = "Role Details")]
        [Required(ErrorMessage = "Details required")]
        public string RoleDetails { get; set; }

        [Display(Name = "Office Term")]
        [Required(ErrorMessage = "Term required")]
        public string OfficeTerm { get; set; }

        [Display(Name = "Campaign start")]
        [RegularExpression(@"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Invalid date format")]
        [Required(ErrorMessage = "Start date required")]
        public string CampaignStart { get; set; }

        [Display(Name = "Campaign end")]
        [RegularExpression(@"^(19|20)\d\d[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])$", ErrorMessage = "Invalid date format")]
        [Required(ErrorMessage = "End date required")]
        public string CampaignEnd { get; set; }

        [Required]
        public string EmployeeId { get; set; }
    }
}