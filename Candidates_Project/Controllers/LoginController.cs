using Candidates_Project.Model;
using Candidates_Project.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Candidates_Project.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepo loginRepo;

        public LoginController(ILoginRepo loginRepo)
        {
            this.loginRepo = loginRepo;
        }

        [HttpPost]
        public IActionResult SaveLogin(Login login)
        {
            var a = loginRepo.SaveLogin(login);
            return Ok(a);
          
        }
    }
}
