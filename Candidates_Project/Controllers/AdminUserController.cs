using Candidates_Project.Implementation;
using Candidates_Project.Model;
using Candidates_Project.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Candidates_Project.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IAdmin_UserRepo admin_UserRepo;

        public AdminUserController(IAdmin_UserRepo admin_UserRepo)
        {
            this.admin_UserRepo = admin_UserRepo;
        }


        [HttpGet]
        public IActionResult GetAllAdminUser()
        {
            var a = admin_UserRepo.GetAllAdminUser();
            if (a.Count > 0)
            {
                return Ok(a);
            }
            return NoContent();


        }

        [HttpGet("{Id}")]
        public IActionResult GetAdminUserById(int Id)
        {
            var a = admin_UserRepo.GetAdminUserById(Id);
            return Ok(a);
        }

        [HttpPost]
        public IActionResult SaveAdminUser(Admin_User admin_User)
        {
            var a = admin_UserRepo.SaveAdminUser(admin_User);
            return Ok(a);
        }

        [HttpPut]

        public IActionResult UpdateAdminUser(Admin_User admin_User)
        {
            var a = admin_UserRepo.UpdateAdminUser(admin_User);
            return Ok(a);
        }
        [HttpDelete]
        public IActionResult DeleteAdminUser(int Id)
        {
            var a = admin_UserRepo.DeleteAdminUser(Id);
            return Ok(a);
        }

    }
}
