using Apahida.Data;
using Apahida.Models;

namespace Apahida.Services
{
    public class UsersService
    {
        private readonly ApahidaContext _context;

        public UsersService(ApahidaContext context)
        {
            _context = context;
        }

        public string LogIn(Users user)
        {
            
            Users u = _context.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (u != null)
            {
                if (u.Role == "Admin")
                    return "Admin";
                else
                    return "Employee";
            }
            else
                return "Error";
        }
    }
}
