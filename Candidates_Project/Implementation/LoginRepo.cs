using Candidates_Project.Model;
using Candidates_Project.Repository;
using Microsoft.Data.SqlClient;
using System.Text;
namespace Candidates_Project.Implementation
{
    public class LoginRepo : ILoginRepo
    {
        private readonly IConfiguration config;
        public LoginRepo(IConfiguration config)
        {
            this.config = config;
        }
        public Response SaveLogin(Login login)
        {
            Response r = new Response();

            try
            {
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "SELECT Password, IsAdmin FROM Candidates_Practice WHERE Email = @Email";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Email", login.email);

                    con.Open();
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        string storedHashedPassword = reader.GetString(reader.GetOrdinal("Password"));
                        bool isAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin"));

                        if (BCrypt.Net.BCrypt.Verify(login.password, storedHashedPassword))
                        {
                            r.resp = true;
                            r.respMsg = "User found";
                            r.respObj = isAdmin;
                            return r;
                        }
                        else
                        {
                            r.resp = false;
                            r.respMsg = "Invalid password";
                            r.respObj = null;
                            return r;
                        }
                    }
                    else
                    {
                        r.resp = false;
                        r.respMsg = "User not found";
                        r.respObj = null;
                        return r;
                    }
                }
            }
            catch (Exception ex)
            {
                r.resp = false;
                r.respMsg = $"An error occurred: {ex.Message}";
                r.respObj = null;
                return r;
            }
        }

    }
}