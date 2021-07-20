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
    [Authorize(Roles ="Admin")]
    public class BranchController : ApiController
    {

        private BranchSelectDbContext db;
        /// <summary>
        /// This methos get all branches from Database
        /// </summary>
        /// <returns>This data is as json</returns>
        public IHttpActionResult Get()
        {
            try
            {
                using (db = new BranchSelectDbContext())
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

        /// <summary>
        /// This method creates classes acording to studten choices
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/branch/CreateClasses")]
        public IHttpActionResult CreateClasses()
        {
            try
            {
                using (db = new BranchSelectDbContext())
                {

                    var firstSelecetNumber = (from s in db.Students
                                              join sb in db.StudentBranches
                                              on s.Id equals sb.StudentId
                                              where s.IsDeleted == false && sb.FirstSelect == 1
                                              select new
                                              { }).Count();

                    var secondSelectNumber = (from s in db.Students
                                              join sb in db.StudentBranches
                                              on s.Id equals sb.StudentId
                                              where s.IsDeleted == false && sb.FirstSelect == 2
                                              select new
                                              { }).Count();

                    var minClassCount = db.Schools.Find(1).MinClassCount;

                    if (firstSelecetNumber >= minClassCount)
                    {
                        var dataFirst = (from s in db.Students
                                         join sb in db.StudentBranches
                                         on s.Id equals sb.StudentId
                                         where s.IsDeleted == false && sb.FirstSelect == 1
                                         select new
                                         {
                                             StudentId = sb.StudentId,

                                         }).ToList();

                        var dataSecond = (from s in db.Students
                                          join sb in db.StudentBranches
                                          on s.Id equals sb.StudentId
                                          where s.IsDeleted == false && sb.FirstSelect == 2
                                          select new
                                          {
                                              StudentId = sb.StudentId,

                                          }).ToList();



                        var studentBranch = db.StudentBranches.ToList();

                        foreach (var item in dataFirst)
                        {
                            studentBranch.Where(sb => sb.StudentId == item.StudentId).FirstOrDefault().Result = 1;
                        }


                        if (secondSelectNumber >= minClassCount)
                        {
                            foreach (var item in dataSecond)
                            {
                                studentBranch.Where(sb => sb.StudentId == item.StudentId).FirstOrDefault().Result = 2;
                            }
                        }
                        else
                        {
                            foreach (var item in dataSecond)
                            {
                                studentBranch.Where(sb => sb.StudentId == item.StudentId).FirstOrDefault().Result = 1;
                            }
                        }


                        db.SaveChanges();
                    }
                    else
                    {
                        var data = db.StudentBranches.ToList();
                        data.ForEach(r => r.Result = 2);
                        db.SaveChanges();
                    }
                    return Ok();
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
