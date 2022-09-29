using API.Context;
using API.Hash;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class AccountRepository : IAccountRepository
    {
        MyContext myContext;
        Hashing hash;
        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
            this.hash = new Hashing();
        }
        
        /* Login
        * Register
        * Change Password
        * Forget Password
       */

        public ResponseLogin Login(LoginVM login)
        {
            var data = myContext.UserRole
                        .Include(x => x.Role)
                        .Include(x => x.User)
                        .Include(x => x.User.Employee)
                        .FirstOrDefault(x =>
                        x.User.Employee.Email == login.Email
                        );
            if (data == null)
                return null;
            //  verify password
            if (!hash.ValidatePassword(login.Password,data.User.Password))
            {
                // authentication failed
                return null;
            }
            else
            {
                ResponseLogin result = new ResponseLogin()
                {
                    Id = data.User.Id,
                    Fullname = data.User.Employee.Fullname,
                    Email = data.User.Employee.Email,
                    Role = data.Role.Name
                };
                return result;
            }
           

            //ResponseLogin result = new ResponseLogin()
            //{
            //    Id = data.User.Id,
            //    Fullname = data.User.Employee.Fullname,
            //    Email = data.User.Employee.Email,
            //    Role = data.Role.Name
            //};
            #region Cara Lain
            //var result = (from e in myContext.Employee
            //              join u in myContext.User
            //              on e.Id equals u.Id
            //              join ur in myContext.UserRole
            //              on e.Id equals ur.UserId
            //              join r in myContext.Role
            //              on ur.RoleId equals r.Id
            //              where e.Email == login.Email
            //              where u.Password == login.Password
            //              select new ResponseLogin
            //              {
            //                  Id = e.Id,
            //                  Fullname = e.Fullname,
            //                  Email = e.Email,
            //                  Role = r.Name,
            //              }).FirstOrDefault();
            //if (result == null)
            //    return null;
            #endregion cara lain

            //return result;
        }

        public int Register(RegisterVM register)
        {
            //saving to Employee table
            Employee employee = new Employee();
            employee.Fullname = register.Fullname;
            employee.Email = register.Email;
            myContext.Employee.Add(employee);
            myContext.SaveChanges();

            //saving to User table
            User user = new User();
            user.Id = employee.Id;
            user.Password = hash.HashPassword(register.Password);
            Console.WriteLine(user.Password);
            myContext.User.Add(user);
            myContext.SaveChanges();

            //saving to UserRole table
            UserRole userRole = new UserRole();
            userRole.UserId = user.Id;
            userRole.RoleId = register.RoleId;
            myContext.UserRole.Add(userRole);
            var result = myContext.SaveChanges();

            return result;
        }

      
        public int ChangePassword(ChangePasswordVM changePassword)
        {
            var data = myContext.User
                       .Include(x => x.Employee)
                       .FirstOrDefault(x =>
                       x.Employee.Email == changePassword.Email
                       );
            // check account and verify password
            if (data==null || !hash.ValidatePassword(changePassword.OldPassword, data.Password))
            {
                // authentication failed
                return -1;
            }
            if (!changePassword.NewPassword.Equals(changePassword.ConfirmNewPassword))
            {
                return -1;
            }
            data.Password = hash.HashPassword(changePassword.NewPassword);
            myContext.User.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public int ForgetPassword(LoginVM login)
        {
            var data = myContext.User
                    .Include(x => x.Employee)
                    .FirstOrDefault(x =>
                    x.Employee.Email == login.Email
                    );
            if (data == null)
                return -1;
            data.Password = hash.HashPassword(login.Password);
            myContext.User.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
