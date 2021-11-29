using IsfahanHC.Models.DataModels;
using IsfahanHC.Models.ViewModels;
using IsfahanHC.Models.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utility;

namespace IsfahanHC.Data.Repository
{
    public enum UserErrorType
    {
        DuplicateUserName,
        DuplicateEmail,
        non,
        NotFound
    }
    public interface IUserRepository
    {
        IList<UserViewModel> GetUsers();
        IList<UserViewModel> GetUsersWhere(Expression<Func<UserViewModel, bool>> filter);
        UserViewModel GetUser(string userName);
        int GetUserId(string userName);
        UserErrorType RegisterNewUser(RegisterViewModel register);
        UserErrorType EditUser(int id,RegisterViewModel register);
        UserErrorType AddUser(User user);
        bool RemoveUser(string userName);

        User GetUserByLogin(string userName, string password);
        bool IsAdmin(int userId);
        bool IsSeller(int userId);
    }

    public class UserRepository : IUserRepository
    {
        IsfahanHCDbContext _context;
        public UserRepository(IsfahanHCDbContext context)
        {
            _context = context;
        }

        public UserErrorType RegisterNewUser(RegisterViewModel register)
        {
            var er = VarifyUser(new User { UserName = register.UserName, Email = register.Email});
            if (er != UserErrorType.non)
                return er;

            var user = new User()
            {
                UserName = register.UserName,
                Email = register.Email.ToLower(),
                Password = HashString.GetHashString(register.Password),
                RegisterTime = DateTime.Now,
                IsAdmin = false,
                IsDeleted = false,
            };
            _context.Add(user);
            _context.SaveChanges();
            return UserErrorType.non;
        }

        public UserErrorType AddUser(User user)
        {
            var er = VarifyUser(user);
            if (er != UserErrorType.non)
                return er;

            user.RegisterTime = DateTime.Now;
            user.Password = HashString.GetHashString(user.Password);
            _context.Add(user);
            _context.SaveChanges();
            return UserErrorType.non;
        }

        private UserErrorType VarifyUser(User user)
        {
            if (_context.Users.Any(u => u.UserId != user.UserId && !u.IsDeleted && u.UserName == user.UserName))
                return UserErrorType.DuplicateUserName;
            else if (_context.Users.Any(u => u.UserId != user.UserId && !u.IsDeleted && u.Email.ToLower() == user.Email.ToLower()))
                return UserErrorType.DuplicateEmail;
            else
                return UserErrorType.non;
        }

        public User GetUserByLogin(string userName, string password)
        {
            password = HashString.GetHashString(password);
            return _context.Users
                     .Include(u => u.Seller)
                     .Where(u => !u.IsDeleted && u.UserName == userName && u.Password == password && !u.IsDeleted)
                     .FirstOrDefault();
        }

        public bool IsAdmin(int userId)
        {
            var user = _context.Users
                .Where(u => !u.IsDeleted && u.UserId == userId)
                .FirstOrDefault();

            if (user == null) return false;
            return user.IsAdmin;
        }

        public bool IsSeller(int userId)
        {
            var seller = _context.Users
                .Include(u => u.Seller)
                .Where(u => !u.IsDeleted && u.UserId == userId)
                .FirstOrDefault()
                .Seller;

            return (seller != null);
        }

        public IList<UserViewModel> GetUsers()
        {
            return _context.Users
                .Include(u => u.Seller)
                .Where(u => !u.IsDeleted)
                .Select(u => new UserViewModel
                {
                    UserName = u.UserName,
                    Email =u.Email,
                    RegisterTime = u.RegisterTime,
                    IsAdmin = u.IsAdmin,
                    IsSeller = (u.Seller != null)
                })
                .ToList();
        }

        public bool RemoveUser(string userName)
        {
            var user = _context.Users
                .Where(u => !u.IsDeleted && u.UserName == userName)
                .FirstOrDefault();

            if (user == null) 
                return false;
            user.IsDeleted = true;
            user.DeleteTime = DateTime.Now;
            _context.SaveChanges();
            return true;
        }

        public UserViewModel GetUser(string userName)
        {
            var user = _context.Users
                .Include(u => u.Seller)
                .ThenInclude(sel => sel.Stor)
                .Where(u => !u.IsDeleted && u.UserName == userName)
                .FirstOrDefault();

            if (user == null)
                return new UserViewModel();

            bool isSeller = user.Seller != null;
            string storName = (isSeller) ? user.Seller.Stor.Name : "";

            return new UserViewModel
            {
                UserName = user.UserName,
                StorName = storName,
                RegisterTime = user.RegisterTime,
                IsAdmin = user.IsAdmin,
                Email = user.Email,
                IsSeller = isSeller
            };
        }

        public IList<UserViewModel> GetUsersWhere(Expression<Func<UserViewModel, bool>> filter)
        {
            return _context.Users
                .Include(u => u.Seller)
                .ThenInclude(u => u.Stor)
                .Where(u => !u.IsDeleted)
                .Select(u => new UserViewModel
                {
                    UserName = u.UserName,
                    Email= u.Email,
                    IsAdmin = u.IsAdmin,
                    IsSeller = u.Seller != null,
                    StorName = (u.Seller != null)?u.Seller.Stor.Name:"",
                    RegisterTime = u.RegisterTime
                })
                .Where(filter)
                .ToList();
        }

        public int GetUserId(string userName)
        {
            var u = _context.Users
                 .Where(u => u.UserName == userName && !u.IsDeleted)
                 .FirstOrDefault();

            return (u == null) ? -1 : u.UserId;
        }

        public UserErrorType EditUser(int id, RegisterViewModel register)
        {
            var er = VarifyUser(new User
            {
                UserId = id,
                UserName = register.UserName,
                Email = register.Email
            });

            if (er != UserErrorType.non)
                return er;

            var user = _context.Users
                .Where(u => u.UserId == id)
                .FirstOrDefault();

            if (user == null)
                return UserErrorType.NotFound;

            user.UserName = (register.UserName?.Trim() != "")? register.UserName: user.UserName;
            user.Email = (register.Email?.Trim() != "") ? register.Email : user.Email; ;
            user.Password = (register.Password?.Trim() != "")? HashString.GetHashString(register.Password): user.Password;
            _context.SaveChanges();
            return UserErrorType.non;
        }
    }
}
