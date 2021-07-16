using BranchSelect.Context;
using BranchSelect.Models;
using BranchSelect.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BranchSelect.Auth
{
    public class UserService : IDisposable
    {
        BranchSelectDbContext db = new BranchSelectDbContext();

        public UserViewModel ValidateUser(string userId,string password)
        {
           
            try
            {
                UserViewModel user = null;

                var data = (from u in db.Users
                            join r in db.Roles
                            on u.RoleId equals r.Id
                            where u.Id == userId && u.Password == password
                            select new
                            {
                                UserId = u.Id,
                                Role = r.Name
                            }).FirstOrDefault();

                if (data != null)
                {
                    user = new UserViewModel();
                    user.UserId = data.UserId;
                    user.Role = data.Role;
                }

                return user;
            }
            catch (Exception e)
            {

                using (var db = new BranchSelectDbContext())
                {
                    var error = new Error();
                    error.Message = e.Message;
                    db.Errors.Add(error);
                    db.SaveChanges();
                }
                return null;
            }

            
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}