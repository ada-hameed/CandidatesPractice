
using Candidates_Project.Model;
using Candidates_Project.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace Candidates_Project.Implementation
{
    public class Admin_UserRepo : IAdmin_UserRepo
    {
        private readonly IConfiguration config;

        public Admin_UserRepo(IConfiguration config)
        {
            this.config = config;
        }

        public List<Admin_User> GetAllAdminUser()
        {
            List<Admin_User> admin_Users = new List<Admin_User>();
            try
            {
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "SELECT * FROM Admin_User_Table";
                    SqlCommand cmd = new SqlCommand(query, con);

                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            Admin_User admin_User = new Admin_User
                            {
                                Id = int.Parse(sdr["Id"].ToString()),
                                Name = sdr["Name"].ToString(),
                                Email = sdr["Email"].ToString(),
                                Password = sdr["Password"].ToString(),
                                IsAdmin = bool.Parse(sdr["IsAdmin"].ToString()),

                                Created_By = sdr["Created_By"].ToString(),

                                
                                Created_On = sdr["Created_On"] != DBNull.Value && DateTime.TryParse(sdr["Created_On"].ToString(), out DateTime createdOn)
                                    ? (DateTime?)createdOn
                                    : null,  

                                Updated_By = sdr["Updated_By"].ToString(),

                                Updated_On = sdr["Updated_On"] != DBNull.Value && DateTime.TryParse(sdr["Updated_On"].ToString(), out DateTime updatedOn)
                                    ? (DateTime?)updatedOn
                                    : null  
                            };

                            admin_Users.Add(admin_User);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                Console.WriteLine(ex.Message);
            }

            return admin_Users;
        }

        public Response GetAdminUserById(int Id)
        {
            Response r = new Response();
            Admin_User admin_User = new Admin_User();

            try
            {
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "SELECT * FROM Admin_User_Table WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", Id);

                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            admin_User.Id = int.Parse(sdr["Id"].ToString());
                            admin_User.Name = sdr["Name"].ToString();
                            admin_User.Email = sdr["Email"].ToString();
                            admin_User.Password = sdr["Password"].ToString();
                            admin_User.IsAdmin = bool.Parse(sdr["IsAdmin"].ToString());

                            //admin_User.Phone = long.Parse(sdr["Phone"].ToString());
                            //admin_User.Age = int.Parse(sdr["Age"].ToString());
                            //admin_User.Gender = sdr["Gender"].ToString();
                            //admin_User.Address = sdr["Address"].ToString();

                            admin_User.Created_By = sdr["Created_By"].ToString();
                            if (sdr["Created_On"] != DBNull.Value && !string.IsNullOrEmpty(sdr["Created_On"].ToString()))
                            {
                                admin_User.Created_On = DateTime.Parse(sdr["Created_On"].ToString());
                            }
                            else
                            {
                                admin_User.Created_On = DateTime.MinValue; // Or use null if it's a nullable DateTime
                            }

                            admin_User.Updated_By = sdr["Updated_By"].ToString();
                            if (sdr["Updated_On"] != DBNull.Value && !string.IsNullOrEmpty(sdr["Updated_On"].ToString()))
                            {
                                admin_User.Updated_On = DateTime.Parse(sdr["Updated_On"].ToString());
                            }
                            else
                            {
                                admin_User.Updated_On = DateTime.MinValue; // Or use null if it's a nullable DateTime
                            }

                        }
                        r.resp = true;
                        r.respMsg = "Successfully fetched.";
                        r.respObj = admin_User;
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

        public Response SaveAdminUser(Admin_User admin_User)
        {
            int RowSaved = 0;
            Response r = new Response();
            try
            {
                if (IfEmailExist(admin_User.Email))
                {
                    r.resp = false;
                    r.respMsg = "Email already exists.";
                    r.respObj = null;
                    return r; // Return early to avoid duplicate entries
                }

                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "INSERT INTO Admin_User_Table (Name, Email, IsAdmin,Password, Created_By, Created_On) VALUES (@Name, @Email, @IsAdmin,@Password, @Created_By, @Created_On);";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Name", admin_User.Name);
                    cmd.Parameters.AddWithValue("@Email", admin_User.Email);
                    cmd.Parameters.AddWithValue("@Password",admin_User.Password);
                    cmd.Parameters.AddWithValue("@IsAdmin", admin_User.IsAdmin);
                    cmd.Parameters.AddWithValue("@Created_By", admin_User.Created_By);
                    cmd.Parameters.AddWithValue("@Created_On", admin_User.Created_On);
                    //cmd.Parameters.AddWithValue("@Phone", admin_User.Phone);
                    //cmd.Parameters.AddWithValue("@Age", admin_User.Age);
                    //cmd.Parameters.AddWithValue("@Gender", admin_User.Gender);
                    //cmd.Parameters.AddWithValue("@Address", admin_User.Address);
                   

                    con.Open();

                    RowSaved = cmd.ExecuteNonQuery();

                    if (RowSaved > 0)
                    {
                        r.resp = true;
                        r.respMsg = "Successfully saved.";
                        r.respObj = admin_User;
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

        public Response UpdateAdminUser(Admin_User admin_User)
        {
            int RowUpdated = 0;
            Response r = new Response();

            try
            {
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "UPDATE Admin_User_Table SET Name = @Name,  Email = @Email,IsAdmin = @IsAdmin,Password = @Password, Updated_By = @Updated_By, Updated_On = @Updated_On WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Name", admin_User.Name);
                    cmd.Parameters.AddWithValue("@Email", admin_User.Email);
                    cmd.Parameters.AddWithValue("@Password", admin_User.Password);
                    cmd.Parameters.AddWithValue("@IsAdmin", admin_User.IsAdmin);
                    //cmd.Parameters.AddWithValue("@Phone", admin_User.Phone);
                    //cmd.Parameters.AddWithValue("@Age", admin_User.Age);
                    //cmd.Parameters.AddWithValue("@Gender", admin_User.Gender);
                    //cmd.Parameters.AddWithValue("@Address", admin_User.Address);
             
                    cmd.Parameters.AddWithValue("@Updated_By", admin_User.Updated_By);
                    cmd.Parameters.AddWithValue("@Updated_On", admin_User.Updated_On);
                    cmd.Parameters.AddWithValue("@Id", admin_User.Id);


                    con.Open();
                    RowUpdated = cmd.ExecuteNonQuery();

                    if (RowUpdated > 0)
                    {
                        r.resp = true;
                        r.respMsg = "Successfully Updated.";
                        r.respObj = admin_User;
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

        public Response DeleteAdminUser(int Id)
        {
            int rowDeleted = 0;
            Response r = new Response();

            try
            {
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "DELETE FROM Admin_User_Table WHERE Id = @Id";

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

        public bool IfEmailExist(string email)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "SELECT * FROM Admin_User_Table WHERE Email = '" + email + "'";
                    SqlCommand cmd = new SqlCommand(query, con);

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
