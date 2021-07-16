using BranchSelect.Context;
using BranchSelect.Models;
using BranchSelect.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace BranchSelect.Controllers
{
   
    public class StudentController : ApiController
    {
        private BranchSelectDbContext db;

        private String FindRole()
        {
            var identity = (ClaimsIdentity)User.Identity;

          
            //Getting the Roles only if you set the roles in the claims
            var role = identity.Claims
                       .Where(c => c.Type == ClaimTypes.Role)
                       .Select(c => c.Value).ToArray();
            return role[0];
        }

        /// <summary>
        /// This Method get data from Studut Table by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The data contains all information</returns>
        [Authorize(Roles ="Admin,User")]
        public IHttpActionResult Get(String id)
        {

            try
            {

                using (db = new BranchSelectDbContext())
                {

                    StudentViewModel student = null;
                    var data = db.Students.Where(s=>s.Id==id && s.IsDeleted==false).FirstOrDefault();
                    if(data!=null)
                    {
                        student = new StudentViewModel();
                        student.Id = data.Id;
                        student.NameAndSurname = data.NameAndSurname;
                        student.Class = data.Class;
                        student.ParentNameAndSurname = data.ParentNameAndSurname;
                        student.Adress = data.Adress;
                        student.Phone = data.Phone;
                        student.Email = data.Email;
                        
                        if (FindRole() == "Admin")
                        {
                            student.IsDeleted = data.IsDeleted;
                        }

                        if (db.StudentBranches.Where(sb=>sb.StudentId==id).Count()>0)
                        {
                            var studentBranchSelect = db.StudentBranches.Find(id);
                            if(studentBranchSelect!=null)
                            {
                                student.FirstSelect = studentBranchSelect.FirstSelect;
                                student.SecondSelect = studentBranchSelect.SecondSelect;
                            }
                        }

                        return Ok(student);
                    }
                    return Ok(student);
                }
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
        }

        /// <summary>
        ///This method fetches all students'infromation.  
        /// </summary>
        /// <returns>This information contains Id,NameAndSurname,FirstChoice and Score</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/student/GetAll")]
        public IHttpActionResult GetAll()
        {
            try
            {
                using (db = new BranchSelectDbContext())
                {

                    //var branches = db.Branches.ToList();
                    var data = (from s in db.Students
                                          select new
                                          {
                                              Id=s.Id,
                                              NameAndSurname=s.NameAndSurname,
                                              Score=s.Score,
                                              IsDeleted=s.IsDeleted
                                          }).Where(s=>s.IsDeleted==false).ToList();

                    List<StudentChoiceViewModel> studentChoices = new List<StudentChoiceViewModel>();
                    String studentFirstSelect;
                    foreach (var item in data)
                    {
                        
                        if (db.StudentBranches.Where(sb => sb.StudentId == item.Id).Count()>0) {

                            var studentBranches = db.StudentBranches.Where(sb => sb.StudentId == item.Id).FirstOrDefault().FirstSelect;
                            studentFirstSelect = db.Branches.Where(b => b.Id == studentBranches).FirstOrDefault().Name;
                            studentChoices.Add(new StudentChoiceViewModel()
                            {
                                Id = item.Id,
                                NameAndSurname = item.NameAndSurname,
                                Choice = studentFirstSelect,
                                Score = item.Score,
                            });

                        }
                        else
                        {
                            studentChoices.Add(new StudentChoiceViewModel()
                            {
                                Id = item.Id,
                                NameAndSurname = item.NameAndSurname,
                                Score = item.Score,
                            });
                        }
                    }       

                    return Ok(studentChoices);
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
        ///This method get numbers  that How many Student choice branch and are there 
        /// </summary>
        /// <returns>finished, unfinished and total student as Paramater</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/student/GetBranchSelection")]
        public IHttpActionResult GetBranchSelection()
        {
            try
            {
                using (db = new BranchSelectDbContext())
                {
                    var data = new ScoreViewModel();
                    data.Finished = (from s in db.Students
                                     join sb in db.StudentBranches
                                     on s.Id equals sb.StudentId
                                     where s.IsDeleted == false
                                     select new
                                     {
                                     }).Count();
                    data.Total = db.Students.Where(s => s.IsDeleted == false).Count();
                    data.UnFinished = data.Total - data.Finished;
                    return Ok(data);
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
        /// This method get number which show instant branch number
        /// </summary>
        /// <returns> FistBranch,FirstBranchNumber,SecondBranch,SecondBranchNumbar ,Total and Status  as Object</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("api/student/GetBranchStatus")]
        public IHttpActionResult GetBranchStatus()
        {
            try
            {
                using (db=new BranchSelectDbContext())
                {
                    var data = new BranchStatusNumberViewModel();
                    data.FirstBranch = db.Branches.Find(1).Name;
                    data.FirstBranchNumber = (from s in db.Students
                                              join sb in db.StudentBranches
                                              on s.Id equals sb.StudentId
                                              where s.IsDeleted==false && sb.FirstSelect==1
                                              select new { }).Count();
                    data.SecondBranch = db.Branches.Find(2).Name;
                    data.SecondBranchNumber= (from s in db.Students
                                              join sb in db.StudentBranches
                                              on s.Id equals sb.StudentId
                                              where s.IsDeleted == false && sb.FirstSelect == 2
                                              select new { }).Count();
                    data.Total = (from s in db.Students
                                  join sb in db.StudentBranches
                                  on s.Id equals sb.StudentId
                                  where s.IsDeleted == false
                                  select new { }).Count();

                    if (data.Total==db.Students.Where(s=>s.IsDeleted==false).Count())
                    {
                        data.Status = true;

                    }
                    else
                    {
                        data.Status = false;
                    }

                    return Ok(data);
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
        /// This methos update student from Student Table
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        
        [Authorize(Roles = "Admin,User")]
        public IHttpActionResult Put(StudentViewModel student)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid Model");

                using (db = new BranchSelectDbContext())
                {

                    if (db.Students.Where(s=>s.Id==student.Id).Count()>0)
                    {

                        var dataStudent = db.Students.Find(student.Id);
                        dataStudent.Id = student.Id;
                        dataStudent.NameAndSurname = student.NameAndSurname;
                        dataStudent.ParentNameAndSurname = student.ParentNameAndSurname;
                        dataStudent.Phone = student.Phone;
                        dataStudent.Score = student.Score;
                        dataStudent.Class = student.Class;
                        dataStudent.Adress = student.Adress;
                        dataStudent.Email = student.Email;
                        if (FindRole() == "Admin")
                        {
                            dataStudent.IsDeleted = student.IsDeleted;
                        }
                        var dataStudentBranch = new StudentBranch();
                        dataStudentBranch.StudentId = dataStudent.Id;
                        dataStudentBranch.FirstSelect = student.FirstSelect;
                        dataStudentBranch.SecondSelect = student.SecondSelect;

                        if (db.StudentBranches.Where(sb => sb.StudentId == dataStudentBranch.StudentId).Count() > 0)
                        {
                            db.Entry(dataStudentBranch).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            db.Entry(dataStudentBranch).State = System.Data.Entity.EntityState.Added;
                        }

                        db.Entry(dataStudent).State = System.Data.Entity.EntityState.Modified;

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
