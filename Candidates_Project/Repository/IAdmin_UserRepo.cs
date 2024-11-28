using Candidates_Project.Model;

namespace Candidates_Project.Repository
{
    public interface IAdmin_UserRepo
    {
        public List<Admin_User> GetAllAdminUser();
        public Response GetAdminUserById(int Id);
        public Response SaveAdminUser(Admin_User admin_User);
        public Response UpdateAdminUser(Admin_User admin_User);
        public Response DeleteAdminUser(int Id);


    }
}
