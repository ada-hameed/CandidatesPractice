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
        public bool SaveLogin(Login login)
        {
            int SavedRow = 0;
            //Response r = new Response();
            try
            {
                string encodedText = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(login.password));
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "SELECT * FROM Admin_User_Table WHERE Email = @Email AND Password = @Password";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Email", login.email);
                    cmd.Parameters.AddWithValue("@Password", encodedText);
                    con.Open();
                    var sdr = cmd.ExecuteReader();
                    if (sdr.HasRows && sdr.Read())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}