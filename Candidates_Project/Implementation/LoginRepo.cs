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
        public Response  SaveLogin(Login login)
        {
            Response r = new Response();
            try
            {
                string encodedText = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(login.password));
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "SELECT isAdmin FROM Candidates_Practice WHERE Email = @Email AND  Password = @Password ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Email", login.email);
                    cmd.Parameters.AddWithValue("@Password", encodedText);
                    
                    con.Open();
                    var sdr = cmd.ExecuteReader();
                    if (sdr.HasRows && sdr.Read())
                    {
                        // Retrieve the value as a boolean
                        bool isAdmin = sdr.GetBoolean(sdr.GetOrdinal("isAdmin"));
                        r.resp = true;
                        r.respMsg = "User found";
                        r.respObj = isAdmin; // Pass the boolean value
                        return r;
                    }
                    else
                    {

                        r.respMsg = "user not found";
                        r.respObj = null;
                        return r;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}