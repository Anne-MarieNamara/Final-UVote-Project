using System;

namespace UVote.Models
{
    public class Vote
    {
        public DateTime VoteDate { get; set; }
        public string CandidateId { get; set; }
        public string StudentNumber { get; set; }
        public int CampaignId { get; set; }
    }
}