using API.Context;
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

        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
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
                        x.User.Employee.Email == login.Email &&
                        x.User.Password.Equals(login.Password));
            if (data == null)
                return null;

            ResponseLogin result = new ResponseLogin()
            {
                Id = data.User.Id,
                Fullname = data.User.Employee.Fullname,
                Email = data.User.Employee.Email,
                Role = data.Role.Name
            };
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
            return result;
        }
    }
}
