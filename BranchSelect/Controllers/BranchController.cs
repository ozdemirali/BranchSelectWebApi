using BranchSelect.Context;
using BranchSelect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BranchSelect.Controllers
{
    public class BranchController : ApiController
    {
        /// <summary>
        /// This methos get all branches from Database
        /// </summary>
        /// <returns>This data is as json</returns>
        public IHttpActionResult Get()
        {
            try
            {
                using (var db = new BranchSelectDbContext())
                {
                    var branches = db.Branches.ToList();

                    return Ok(branches);
                }
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

                return Ok(e.Message);
            }
            
        }
    }
}
