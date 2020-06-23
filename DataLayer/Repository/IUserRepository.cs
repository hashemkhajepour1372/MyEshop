using DataLayer.AccountViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
  public  interface IUserRepository
    {
        IEnumerable<Users> GetAllUsers();
        Users GetUserById(int userId);
        bool InsertUser(Users user);
        bool UpdateUser(Users user);
        bool DeleteUser(Users user);
        bool DeleteUser(int userId);
        void save();

        IEnumerable<RegisterViewModel> GetUsersForView();

        Users GetUserByActivecode(string userId);

        Users GetUserByEmailandpassword(string email, string password);

        bool IsValidEmail(string email);
    }
}
