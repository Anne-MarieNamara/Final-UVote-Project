using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Helpers;
using System.Web.Configuration;
using System.IO;
using System.Xml;

namespace UVote.Models
{
    public class DAO
    {
        SqlConnection connection;
        public string message = string.Empty;

        // Initialize the connection object
        public void Connection()
        {
            connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["connectionStringLocal"].ConnectionString);
        }

        #region Student
        public int Insert(StudentModel model)
        {
            int count = 0;
            SqlCommand cmd;
            string password;
            Connection();
            cmd = new SqlCommand("uspInsertStudent", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@studentNumber", model.StudentNumber);
            cmd.Parameters.AddWithValue("@firstName", model.FirstName);
            cmd.Parameters.AddWithValue("@lastName", model.LastName);
            cmd.Parameters.AddWithValue("@telephone", model.Telephone);
            cmd.Parameters.AddWithValue("@email", model.Email);
            cmd.Parameters.AddWithValue("@addressLine1", model.AddressLine1);
            cmd.Parameters.AddWithValue("@addressLine2", model.AddressLine2);
            cmd.Parameters.AddWithValue("@addressLine3", model.AddressLine3);
            cmd.Parameters.AddWithValue("@addressLine4", model.AddressLine4);
            cmd.Parameters.AddWithValue("@employeeId", model.EmployeeId);
            password = Crypto.HashPassword(model.Password);
            cmd.Parameters.AddWithValue("@password", password);

            try
            {
                connection.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return count;
        }
        #endregion

        #region Admin
        // Create an admin
        public int InsertAdmin(AdminModel adminModel)
        {
            int count = 0;
            SqlCommand cmd;
            string password;
            Connection();
            cmd = new SqlCommand("uspInsertAdmin", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@employeeId", adminModel.EmployeeId);
            cmd.Parameters.AddWithValue("@name", adminModel.Name);
            cmd.Parameters.AddWithValue("@email", adminModel.Email);
            password = Crypto.HashPassword(adminModel.Password);
            cmd.Parameters.AddWithValue("@password", password);

            try
            {
                connection.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return count;
        }

        // Login an admin
        public string AdminLogin(AdminModel adminModel)
        {
            string employeeId = null;
            string name = null;
            string password;
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspAdminLogin", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", adminModel.Email);

            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    password = reader["Password"].ToString();
                    if (Crypto.VerifyHashedPassword(password, adminModel.Password))
                    {
                        employeeId = reader["EmployeeId"].ToString();
                        name = reader["Name"].ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return employeeId;
        }

        #endregion

        #region Campaign
        // Create a campaign
        public int InsertCampaign(CampaignModel campaignModel)
        {
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspInsertCampaign", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@roleTitle", campaignModel.RoleTitle);
            cmd.Parameters.AddWithValue("@roleDetails", campaignModel.RoleDetails);
            cmd.Parameters.AddWithValue("@officeTerm", campaignModel.OfficeTerm);
            cmd.Parameters.AddWithValue("@campaignStart", campaignModel.CampaignStart);
            cmd.Parameters.AddWithValue("@campaignEnd", campaignModel.CampaignEnd);
            cmd.Parameters.AddWithValue("@employeeId", campaignModel.EmployeeId);

            try
            {
                connection.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return count;
        }

        // Get all campaigns
        public List<Campaign> GetCampaigns()
        {
            List<Campaign> campaigns = new List<Campaign>();
            SqlDataReader reader;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspGetCampaigns", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Campaign campaign = new Campaign();
                    campaign.CampaignId = reader[0].ToString();
                    campaign.RoleTitle = reader[1].ToString();
                    campaign.RoleDetails = reader[2].ToString();
                    campaign.OfficeTerm = reader[3].ToString();
                    campaign.CampaignStart = reader[4].ToString();
                    campaign.CampaignEnd = reader[5].ToString();
                    campaign.EmployeeId = reader[6].ToString();
                    campaigns.Add(campaign);
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return campaigns;
        }

        public List<Campaign> GetCampaignDropDown()
        {
            List<Campaign> campaigns = new List<Campaign>();
            SqlDataReader reader;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspGetCampaignDropDown", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            
            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Campaign campaign = new Campaign();
                    campaign.CampaignId = reader[0].ToString();
                    campaign.RoleTitle = reader[1].ToString();
                    campaigns.Add(campaign);
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return campaigns;
        }
        #endregion

        #region Candidate
        public int InsertCandidate(CandidateModel candidateModel)
        {
            int count = 0;
            DataSet ds = GetPreviousHistoryData(candidateModel);
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspInsertCandidate", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@candidateId", candidateModel.CandidateId);
            cmd.Parameters.AddWithValue("@firstName", candidateModel.FirstName);
            cmd.Parameters.AddWithValue("@lastName", candidateModel.LastName);
            cmd.Parameters.AddWithValue("@manifesto", candidateModel.Manifesto);
            cmd.Parameters.AddWithValue("@imageUrl", candidateModel.ImageUrl);
            //cmd.Parameters.AddWithValue("@previousHistory", candidateModel.PreviousHistory);
            cmd.Parameters.AddWithValue("@previousHistory", ds.GetXml());
            cmd.Parameters.AddWithValue("@campaignId", candidateModel.CampaignId);
            cmd.Parameters.AddWithValue("@employeeId", candidateModel.EmployeeId);

            try
            {
                connection.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return count;
        }

        private DataSet GetPreviousHistoryData(CandidateModel candidateModel)
        {
            DataSet ds = new DataSet("Histories");
            DataTable dt = new DataTable("History");
            DataRow row;
            dt.Columns.Add("Description");
            ds.Tables.Add(dt);
            string history = candidateModel.PreviousHistory;
            row = dt.NewRow();
            row["Description"] = history;
            dt.Rows.Add(row);
            ds.AcceptChanges();
            return ds;
        }

        // Get all campaign ids
        public IEnumerable<int> GetAllCampaignID()
        {
            List<int> ids = new List<int>();
            SqlDataReader reader;
            SqlCommand cmd;
            int campaignId;
            Connection();
            cmd = new SqlCommand("uspGetAllCampaignID", connection);

            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    campaignId = Convert.ToInt32(reader[0]);
                    ids.Add(campaignId);
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            return ids;
        }

        // Get all candidates for admin
        public List<CandidateForAdmin> GetAllCandidatesForAdmin()
        {
            List<CandidateForAdmin> list = new List<CandidateForAdmin>();
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetAllCandidatesForAdmin", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CandidateForAdmin candidate = new CandidateForAdmin();
                    candidate.CandidateId = Convert.ToInt32(reader["CandidateId"].ToString());
                    candidate.FirstName = reader["FirstName"].ToString();
                    candidate.LastName = reader["LastName"].ToString();
                    candidate.RoleTitle = reader["RoleTitle"].ToString();
                    candidate.ImageUrl = reader["ImageUrl"].ToString();
                    list.Add(candidate);
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return list;
        }
        #endregion

        #region Voter
        // Login an voter
        public string VoterLogin(VoterModel voterModel)
        {
            string studentNumber = null;
            string password;
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspVoterLogin", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@studentNumber", voterModel.StudentNumber);

            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    password = reader["Password"].ToString();
                    if (Crypto.VerifyHashedPassword(password, voterModel.Password))
                    {
                        studentNumber = reader["StudentNumber"].ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return studentNumber;
        }

        // Check to see if user has voted.
        public int CheckIfUserVoted(string studentNumber, int campaignId)
        {
            int count = 0;
            SqlDataReader reader;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspDidUserVote", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@studentNumber", studentNumber);
            cmd.Parameters.AddWithValue("@campaignId", campaignId);
            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    count = 1;
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return count;
        }

        // Get all pending elections
        public List<Election> GetElections()
        {
            List<Election> elections = new List<Election>();
            SqlDataReader reader;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspGetElections", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Election election = new Election();
                    election.CampaignId = reader[0].ToString();
                    election.RoleTitle = reader[1].ToString();
                    elections.Add(election);
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return elections;
        }

        // Get all campaigns
        public List<ElectoralCandidate> GetElectionCandidates(int campaignId)
        {
            List<ElectoralCandidate> list = new List<ElectoralCandidate>();
            SqlDataReader reader;
            SqlCommand cmd;
            //DataRow row;
            Connection();
            cmd = new SqlCommand("uspGetElectionCandidates", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@campaignId", campaignId);

            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ElectoralCandidate candidate = new ElectoralCandidate();
                    DataSet ds = new DataSet();
                    candidate.CandidateId = reader[0].ToString();
                    candidate.FirstName = reader[1].ToString();
                    candidate.LastName = reader[2].ToString();
                    candidate.Manifesto = reader[3].ToString();
                    candidate.ImageUrl = reader[4].ToString();
                    list.Add(candidate);
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return list;
        }

        // Insert a vote
        public int InsertVote(Vote vote)
        {
            int count = 0;
            SqlCommand cmd;
            Connection();
            cmd = new SqlCommand("uspInsertVote", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            vote.VoteDate = DateTime.Now;
            cmd.Parameters.AddWithValue("@date", vote.VoteDate);
            cmd.Parameters.AddWithValue("@candidateId", vote.CandidateId);
            cmd.Parameters.AddWithValue("@studentNumber", vote.StudentNumber);
            cmd.Parameters.AddWithValue("@campaignId", vote.CampaignId);

            try
            {
                connection.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return count;
        }
        #endregion

        #region Results
        // Display all running and ended campaigns
        public List<RunningAndEndedElection> GetAllRunningAndEndedElections()
        {
            List<RunningAndEndedElection> elections = new List<RunningAndEndedElection>();
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetAllRunningAndEndedCampaigns", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RunningAndEndedElection election = new RunningAndEndedElection();
                    election.CampaignId = Convert.ToInt32(reader[0].ToString());
                    election.RoleTitle = reader[1].ToString();
                    elections.Add(election);
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return elections;
        }

        // Display results of all elections
        public List<Result> GetResults(int campaignId)
        {
            List<Result> results = new List<Result>();
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetElectionResults", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@campaignId", campaignId);

            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Result result = new Result();
                    result.FirstName = reader["FirstName"].ToString();
                    result.LastName = reader["LastName"].ToString();
                    result.ImageUrl = reader["ImageUrl"].ToString();
                    result.Manifesto = reader["Manifesto"].ToString();
                    result.Votes = Convert.ToInt32(reader["Total"]);
                    results.Add(result);
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return results;
        }

        // Get the role title to populate the <h2> tag on /Result/Index
        public string GetRoleTitle(int campaignId)
        {
            string title = null;
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetCampaignRoleTitle", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@campaignId", campaignId);

            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    title = reader["RoleTitle"].ToString();
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return title;
        }

        // Get the office term to populate the <h4> tag on /Result/Index
        public string GetOfficeTerm(int campaignId)
        {
            string officeTerm = null;
            SqlCommand cmd;
            SqlDataReader reader;
            Connection();
            cmd = new SqlCommand("uspGetCampaignOfficeTerm", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@campaignId", campaignId);

            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    officeTerm = reader["OfficeTerm"].ToString();
                }
            }
            catch (SqlException ex)
            {
                message = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            return officeTerm;
        }
        #endregion
    }
}