CREATE PROC uspGetAllCandidatesForAdmin
AS
SELECT Campaign.RoleTitle, Candidate.CandidateId,
Candidate.FirstName, Candidate.LastName, 
Candidate.ImageUrl
FROM Campaign
JOIN Candidate ON
Campaign.CampaignId = Candidate.CampaignId

exec uspGetAllCandidatesForAdmin