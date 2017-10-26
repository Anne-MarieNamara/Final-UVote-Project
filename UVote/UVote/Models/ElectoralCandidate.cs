using System.ComponentModel.DataAnnotations;

namespace UVote.Models
{
    public class ElectoralCandidate
    {
        [Display(Name = "Candidate ID")]
        [RegularExpression(@"^(\d{8})$")]
        [Required]
        public string CandidateId { get; set; }

        [Display(Name = "First name")]
        [RegularExpression(@"^[A-Za-z]+$")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [RegularExpression(@"^[A-Za-z]+$")]
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Manifesto { get; set; }


        [Display(Name = "Avatar")]
        public string ImageUrl { get; set; }

        [Display(Name = "Previous history")]
        [Required]
        public string PreviousHistory { get; set; }

        [Display(Name = "Campaign")]
        [Required]
        public string CampaignId { get; set; }
    }
}