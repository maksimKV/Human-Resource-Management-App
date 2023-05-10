using Human_Resource_Management_App.Context;
using Human_Resource_Management_App.Entities;
using Human_Resource_Management_App.Helpers;

namespace Human_Resource_Management_App.Services
{
    public interface IAdminService
    {
        Task<Admin> Authenticate(string email, string password);
    }
    public class AdminService : IAdminService
    {
        private List<Admin> _admins;

        public AdminService(DataContext context)
        {
            _admins = context.Admins.ToList();
        }

        public async Task<Admin> Authenticate(string email, string password)
        {
            Admin admin = await Task.Run(() => _admins.SingleOrDefault(x => x.Email == email && x.Password == password));

            // return null if admin not found
            if (admin == null)
                return null;

            // authentication successful so return admin details without password
            return admin.WithoutPassword();
        }
    }
}
