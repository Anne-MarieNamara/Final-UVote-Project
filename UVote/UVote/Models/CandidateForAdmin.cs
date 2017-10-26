using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UVote.Models
{
    public class CandidateForAdmin
    { 
            public string RoleTitle { get; set; }
            public int CandidateId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ImageUrl { get; set; }   
    }
}