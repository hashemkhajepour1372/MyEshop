using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.AccountViewModel;
namespace DataLayer
{
    public class UserRepository : IUserRepository
    {
       private MyEshop_DBEntities db;
        public UserRepository(MyEshop_DBEntities context)
        {
            this.db = context;
        }

        public bool DeleteUser(Users user)
        {
            try
            {
                db.Entry(user).State = EntityState.Deleted;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                var user = GetUserById(userId);
                DeleteUser(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return db.Users;
        }

        public Users GetUserByActivecode(string userId)
        {
            return db.Users.SingleOrDefault(u => u.ActiveCode == userId);
        }

        public Users GetUserByEmailandpassword(string email, string password)
        {
            return db.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
        }

        public Users GetUserById(int userId)
        {
            return db.Users.Find(userId);
        }

        public IEnumerable<RegisterViewModel> GetUsersForView()
        {
            return db.Users.Select(u => new RegisterViewModel()
            {
                UserName = u.UserName,
                Email = u.Email,
                Password = u.Password,
                Repasword = u.Password
            });
        }

        public bool InsertUser(Users user)
        {
            try
            {
                db.Users.Add(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsValidEmail(string email)
        {

            bool IsEmail =   db.Users.Any(u => u.Email == email.Trim().ToLower());
                return IsEmail;
           
        }

        public void save()
        {
            db.SaveChanges();
        }

        public bool UpdateUser(Users user)
        {
            try
            {
                db.Entry(user).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
