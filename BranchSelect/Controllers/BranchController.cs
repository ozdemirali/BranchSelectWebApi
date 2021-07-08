using BranchSelect.Context;
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
            catch (Exception)
            {

                return NotFound();
            }
            
        }
    }
}
