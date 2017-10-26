using System.ComponentModel.DataAnnotations;

namespace UVote.Models
{
    public class CampaignDropDown
    {
        public string CampaignId { get; set; }
        [Required(ErrorMessage = "Select a campaign")]
        public string RoleTitle { get; set; }
    }
}