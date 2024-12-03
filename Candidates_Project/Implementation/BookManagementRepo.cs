
using Candidates_Project.Model;
using Candidates_Project.Repository;
using Microsoft.Data.SqlClient;

namespace Candidates_Project.Implementation
{
    public class BookManagementRepo:IBookManagementRepo
    {
        private readonly IConfiguration config;

        public BookManagementRepo(IConfiguration config)
        {
            this.config = config;
        }

        public List<BookManagement> GetAllBooks()
            {
            List<BookManagement> bookList = new List<BookManagement>();
            try
            {
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "SELECT * FROM Candidates_Books";
                    SqlCommand cmd = new SqlCommand(query, con);

                    con.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            BookManagement bookManagement = new BookManagement
                            {
                                Book_Id = int.Parse(sdr["Book_Id"].ToString()),
                                Title = sdr["Title"].ToString(),
                                Author = sdr["Author"].ToString(),
                                Category = sdr["Category"].ToString(),
                                Quantity= int.Parse(sdr["Quantity"].ToString()),
                                PublicationDate = DateOnly.FromDateTime(DateTime.Parse(sdr["PublicationDate"].ToString())),
                                Created_On = sdr["Created_On"] != DBNull.Value && DateTime.TryParse(sdr["Created_On"].ToString(), out DateTime createdOn)
                                    ? (DateTime?)createdOn
                                    : null,

                                Updated_By = sdr["Updated_By"].ToString(),

                                Updated_On = sdr["Updated_On"] != DBNull.Value && DateTime.TryParse(sdr["Updated_On"].ToString(), out DateTime updatedOn)
                                    ? (DateTime?)updatedOn
                                    : null
                            };
                            bookList.Add(bookManagement);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return bookList;
        }

        public Response GetByBookId(int Book_Id)
        {
            Response r = new Response();
            BookManagement bookManagement = new BookManagement();
            try
            {
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "SELECT * FROM Candidates_Books WHERE Book_Id = @Book_Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Book_Id", Book_Id);
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            bookManagement.Book_Id = int.Parse(sdr["Book_Id"].ToString());
                            bookManagement.Title = sdr["Title"].ToString();
                            bookManagement.Author = sdr["Author"].ToString();
                            bookManagement.Category = sdr["Category"].ToString();
                            bookManagement.Quantity = int.Parse(sdr["Quantity"].ToString());
                            bookManagement.PublicationDate = DateOnly.Parse(sdr["PublicationDate"].ToString().Split(' ')[0]);
                            DateTime dateTime = DateTime.Parse(sdr["PublicationDate"].ToString());
                            bookManagement.PublicationDate = DateOnly.FromDateTime(dateTime);

                            bookManagement.Created_By = sdr["Created_By"].ToString();
                            if (sdr["Created_On"] != DBNull.Value && !string.IsNullOrEmpty(sdr["Created_On"].ToString()))
                            {
                                bookManagement.Created_On = DateTime.Parse(sdr["Created_On"].ToString());
                            }
                            else
                            {
                                bookManagement.Created_On = DateTime.MinValue;
                            }

                            bookManagement.Updated_By = sdr["Updated_By"].ToString();
                            if (sdr["Updated_On"] != DBNull.Value && !string.IsNullOrEmpty(sdr["Updated_On"].ToString()))
                            {
                                bookManagement.Updated_On = DateTime.Parse(sdr["Updated_On"].ToString());
                            }
                            else
                            {
                                bookManagement.Updated_On = DateTime.MinValue;
                            }

                        }
                        r.resp = true;
                        r.respMsg = "Successfully fetched.";
                        r.respObj = bookManagement;
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

        public Response SaveBook(BookManagement bookManagement)
        {
            Response r = new Response();
            int RowSaved = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "INSERT INTO Candidates_Books (Title, Author, Category, Quantity, PublicationDate,Created_By,Created_On)VALUES (@Title,@Author,@Category,@Quantity,@PublicationDate,@Created_By,@Created_On)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Title", bookManagement.Title);
                    cmd.Parameters.AddWithValue("@Author", bookManagement.Author);
                    cmd.Parameters.AddWithValue("@Category", bookManagement.Category);
                    cmd.Parameters.AddWithValue("@Quantity", bookManagement.Quantity);
                    cmd.Parameters.AddWithValue("@PublicationDate", bookManagement.PublicationDate);
                    cmd.Parameters.AddWithValue("@Created_By", bookManagement.Created_By);
                    cmd.Parameters.AddWithValue("@Created_On", bookManagement.Created_On);
                    con.Open();

                    RowSaved = cmd.ExecuteNonQuery();

                    if (RowSaved > 0)
                    {
                        r.resp = true;
                        r.respMsg = "Successfully Book Saved.";
                        r.respObj = bookManagement;
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

        public Response UpdateBook(BookManagement bookManagement)
        {
            Response r = new Response();
            int RowUpdated = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "UPDATE Candidates_Books SET Title = @Title, Author = @Author,Category = @Category,Quantity = @Quantity,PublicationDate=@PublicationDate,Updated_By = @Updated_By,Updated_On= Updated_On WHERE Book_Id = @Book_Id";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Title", bookManagement.Title);
                    cmd.Parameters.AddWithValue("@Author", bookManagement.Author);
                    cmd.Parameters.AddWithValue("@Category", bookManagement.Category);
                    cmd.Parameters.AddWithValue("@Quantity", bookManagement.Quantity);
                    cmd.Parameters.AddWithValue("@PublicationDate", bookManagement.PublicationDate);
                    cmd.Parameters.AddWithValue("@Updated_By", bookManagement.Updated_By);
                    cmd.Parameters.AddWithValue("@Updated_On", bookManagement.Updated_On);
                    cmd.Parameters.AddWithValue("@Book_Id", bookManagement.Book_Id);
                    con.Open();

                    RowUpdated = cmd.ExecuteNonQuery();

                    if (RowUpdated > 0)
                    {
                        r.resp = true;
                        r.respMsg = "Successfully updated.";
                        r.respObj = bookManagement;
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

        public Response DeleteBook(int Book_Id)
        {
            int rowDeleted = 0;
            Response r = new Response();

            try
            {
                using (SqlConnection con = new SqlConnection(config.GetConnectionString("AlphaDev")))
                {
                    string query = "DELETE FROM Candidates_Books WHERE Book_Id = @Book_Id";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Book_Id", Book_Id);

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
