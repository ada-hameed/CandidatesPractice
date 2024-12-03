using Candidates_Project.Model;

namespace Candidates_Project.Repository
{
    public interface IBookManagementRepo
    {
        public List<BookManagement> GetAllBooks();
        public Response GetByBookId(int Book_Id);
        public Response SaveBook(BookManagement bookManagement);
        public Response UpdateBook(BookManagement bookManagement);
        public Response DeleteBook(int Book_Id);
    }
}
