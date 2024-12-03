using Candidates_Project.Implementation;
using Candidates_Project.Model;
using Candidates_Project.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Candidates_Project.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookManagementController : ControllerBase
    {
        private readonly IBookManagementRepo bookManagementRepo;

        public BookManagementController(IBookManagementRepo bookManagementRepo)
        {
            this.bookManagementRepo = bookManagementRepo;
        }
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = bookManagementRepo.GetAllBooks();
            if (books.Count > 0)
            {

                return Ok(books);
            }
            return NoContent();
        }

        [HttpGet("{Book_Id}")]
        public IActionResult GetByBookId(int Book_Id)
        {
            var books = bookManagementRepo.GetByBookId(Book_Id);
            return Ok(books);
        }
        [HttpPost]
        public IActionResult SaveBook(BookManagement bookManagement)
        {
            var books = bookManagementRepo.SaveBook(bookManagement);
            {
                return Ok(books);
            }
        }

        [HttpPut]
        public IActionResult UpdateBook(BookManagement bookManagement)
        {
            var books = bookManagementRepo.UpdateBook(bookManagement);
            return Ok(books);
        }
        [HttpDelete("{Book_Id}")]

        public IActionResult DeleteBook(int Book_Id)
        {
            var book = bookManagementRepo.DeleteBook(Book_Id);
            return Ok(book);
        }


    }
}
