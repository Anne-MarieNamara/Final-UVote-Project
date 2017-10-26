using System.ComponentModel.DataAnnotations;

namespace UVote.Models
{
    public class Election
    {
        [Required]
        public string CampaignId { get; set; }

        [Required]
        public string RoleTitle { get; set; }
    }
}