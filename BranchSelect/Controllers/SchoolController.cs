using BranchSelect.Context;
using BranchSelect.Models;
using BranchSelect.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BranchSelect.Controllers
{
    public class SchoolController : ApiController
    {
        //private BranchSelectDbContext db;
        public IHttpActionResult Post(SetupViewModel setup)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                using (var db = new BranchSelectDbContext())
                {
                    var dataSchool = new School();
                    var dataBranch = new List<Branch>();
                    dataSchool.Id = 1;
                    dataSchool.Name = setup.SchoolName;
                    dataSchool.BranchTeacher = setup.BranchTeacher;
                    dataSchool.AssistantDirector = setup.AsistantDirector;

                    if (db.Schools.Where(s => s.Id == dataSchool.Id).Count() > 0)
                    {
                        db.Entry(dataSchool).State = System.Data.Entity.EntityState.Modified;
                    }
                    else
                    {
                        db.Entry(dataSchool).State = System.Data.Entity.EntityState.Added;
                    }



                    dataBranch.Add(
                        new Branch()
                        {
                            Id = 1,
                            Name = setup.FirstBranch
                        });
                    dataBranch.Add(
                       new Branch()
                       {
                           Id = 2,
                           Name = setup.SecondBranch
                       });

                    foreach (var item in dataBranch)
                    {
                        if (db.Branches.Where(b => b.Id == item.Id).Count() > 0)
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            db.Entry(item).State = System.Data.Entity.EntityState.Added;
                        }

                    }
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
            
        }
    }
}
