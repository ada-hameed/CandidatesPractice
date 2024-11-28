using Candidates_Project.Model;
using Candidates_Project.Repository;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
namespace Candidates_Project.Implementation
{
    public class CandidateRepo : ICandidateRepo
    {
        private readonly IConfiguration config;

        public CandidateRepo(IConfiguration config)
        {
            this.config = config;
        }

        public List<Candidate> GetAllCandidates()
        {
            List<Candidate> candidates = new List<Candidate>();

            try
            {
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "SELECT * FROM Candidates_Practice";
                    SqlCommand cmd = new SqlCommand(query, con);

                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            Candidate candidate = new Candidate
                            {
                                Id = int.Parse(sdr["Id"].ToString()),
                                Name = sdr["Name"].ToString(),
                                Email = sdr["Email"].ToString(),
                                Phone = long.Parse(sdr["Phone"].ToString()),
                                Age = int.Parse(sdr["Age"].ToString()),
                                Gender = sdr["Gender"].ToString(),
                                Address = sdr["Address"].ToString()
                            };
                            candidates.Add(candidate);
                        }
                    }
                }
            }

            catch (Exception ex)
            {

                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return candidates;
        }

        public Response GetCandidateById(int Id)
        {
            Response r = new Response();
            Candidate candidate = new Candidate();

            try
            {
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "SELECT * FROM Candidates_Practice WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", Id);

                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            candidate.Id = int.Parse(sdr["Id"].ToString());
                            candidate.Name = sdr["Name"].ToString();
                            candidate.Email = sdr["Email"].ToString();
                            candidate.Phone = long.Parse(sdr["Phone"].ToString());
                            candidate.Age = int.Parse(sdr["Age"].ToString());
                            candidate.Gender = sdr["Gender"].ToString();
                            candidate.Address = sdr["Address"].ToString();
                        }
                        r.resp = true;
                        r.respMsg = "Successfully fetched.";
                        r.respObj = candidate;
                    }
                    else
                    {
                        r.resp = false;
                        r.respMsg = "Record not found.";
                        r.respObj = null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any other errors
                r.resp = false;
                r.respMsg = "An error occurred: " + ex.Message;
                r.respObj = null;
            }

            return r;
        }

        public Response SaveCandidate(Candidate candidate)
        {
            int rowSaved = 0;
            Response r = new Response();

            try
            {
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "INSERT INTO Candidates_Practice (Name, Phone,Email, Age, Gender, Address) VALUES (@Name, @Phone,@Email, @Age, @Gender, @Address);";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Name", candidate.Name);
                    cmd.Parameters.AddWithValue("@Phone", candidate.Phone);
                    cmd.Parameters.AddWithValue("@Email", candidate.Email);
                    cmd.Parameters.AddWithValue("@Age", candidate.Age);
                    cmd.Parameters.AddWithValue("@Gender", candidate.Gender);
                    cmd.Parameters.AddWithValue("@Address", candidate.Address);

                    con.Open();

                    rowSaved = cmd.ExecuteNonQuery();

                    if (rowSaved > 0)
                    {
                        r.resp = true;
                        r.respMsg = "Successfully saved.";
                        r.respObj = candidate;
                    }

                }
            }

            catch (Exception ex)
            {

                r.resp = false;
                r.respMsg = "An error occurred: " + ex.Message;
                r.respObj = null;
            }

            return r;
        }

        public Response UpdateCandidate(Candidate candidate)
        {
            int rowUpdated = 0;
            Response r = new Response();

            try
            {
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "UPDATE Candidates_Practice SET Name = @Name, Phone = @Phone, Email = @Email, Age = @Age, Gender = @Gender, Address = @Address WHERE Id = @Id";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Name", candidate.Name);
                    cmd.Parameters.AddWithValue("@Phone", candidate.Phone);
                    cmd.Parameters.AddWithValue("@Email", candidate.Email);
                    cmd.Parameters.AddWithValue("@Age", candidate.Age);
                    cmd.Parameters.AddWithValue("@Gender", candidate.Gender);
                    cmd.Parameters.AddWithValue("@Address", candidate.Address);
                    cmd.Parameters.AddWithValue("@Id", candidate.Id);

                    con.Open();

                    rowUpdated = cmd.ExecuteNonQuery();

                    if (rowUpdated > 0)
                    {
                        r.resp = true;
                        r.respMsg = "Successfully updated.";
                        r.respObj = candidate;
                    }
                    else
                    {
                        r.resp = false;
                        r.respMsg = "Update failed.";
                        r.respObj = null;
                    }
                }
            }

            catch (Exception ex)
            {

                r.resp = false;
                r.respMsg = "An error occurred: " + ex.Message;
                r.respObj = null;
            }

            return r;
        }

        public Response DeleteCandidate(int Id)
        {
            int rowDeleted = 0;
            Response r = new Response();

            try
            {
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "DELETE FROM Candidates_Practice WHERE Id = @Id";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", Id);

                    con.Open();

                    rowDeleted = cmd.ExecuteNonQuery();

                    if (rowDeleted > 0)
                    {
                        r.resp = true;
                        r.respMsg = "Successfully deleted.";
                    }
                    else
                    {
                        r.resp = false;
                        r.respMsg = "Delete failed. Record not found.";
                    }
                }
            }

            catch (Exception ex)
            {
                r.resp = false;
                r.respMsg = "An error occurred: " + ex.Message;
            }

            return r;
        }

    }
}
