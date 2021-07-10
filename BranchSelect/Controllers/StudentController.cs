﻿using BranchSelect.Context;
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

    public class StudentController : ApiController
    {
        private BranchSelectDbContext db;


        /// <summary>
        /// This Method get data from Studut Table by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The data contains all information</returns>
        public IHttpActionResult Get(String id)
        {
            try
            {

                using (db = new BranchSelectDbContext())
                {
                    //var data = db.Students.Find(id);
                    var student = (from s in db.Students
                                   select new
                                    {
                                        Id=s.Id,
                                        NameAndSurname=s.NameAndSurname,
                                        Class = s.Class,
                                        ParentNameAndSurname = s.ParentNameAndSurname,
                                        Adress = s.Adress,
                                        Phone = s.Phone,
                                        Email = s.Email,
                                    }).Where(s=>s.Id==id).FirstOrDefault();
                    
                    return Ok(student);

                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        /// <summary>
        ///This method fetches all students'infromation.  
        /// </summary>
        /// <returns>This information contains Id,NameAndSurname,FirstChoice and Score</returns>
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
                                          join sb in db.StudentBranches
                                          on s.Id equals sb.StudentId
                                          select new
                                          {
                                              Id=s.Id,
                                              NameAndSurname=s.NameAndSurname,
                                              Score=s.Score,
                                              FirstSelect = sb.FirstSelect,
                                          }).ToList();

                    List<StudentChoiceViewModel> studentChoices = new List<StudentChoiceViewModel>();
                    foreach (var item in data)
                    {
                        studentChoices.Add(new StudentChoiceViewModel()
                        {
                            Id=item.Id,
                            NameAndSurname=item.NameAndSurname,
                            Choice=db.Branches.Where(b=>b.Id==item.FirstSelect).FirstOrDefault().Name,
                            Score=item.Score,
                        });
                    }       

                    return Ok(studentChoices);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        /// <summary>
        ///This method get numbers  that How many Student choice branch and are there 
        /// </summary>
        /// <returns>finished, unfinished and total student</returns>
        [HttpGet]
        [Route("api/student/GetBranchSelection")]
        public IHttpActionResult GetBranchSelection()
        {
            try
            {
                using (db = new BranchSelectDbContext())
                {
                    var data = new ScoreViewModel();
                    data.Finished = db.StudentBranches.Count();
                    data.Total = db.Students.Count();
                    data.UnFinished = data.Total - data.Finished;
                    return Ok(data);
                }
            }
            catch (Exception)
            {

                return NotFound();
            }

           
        }

        /// <summary>
        /// This methos update student from Student Table
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public IHttpActionResult Put(StudentViewModel student)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Not a valid Model");

                using (db = new BranchSelectDbContext())
                {
                    var data = new Student();
                    data.Id = student.Id;
                    data.NameAndSurname = student.NameAndSurname;
                    data.ParentNameAndSurname = student.ParentNameAndSurname;
                    data.Phone = student.Phone;
                    data.Score = student.Score;
                    data.Class = student.Class;
                    data.Adress = student.Adress;
                    data.Email = student.Email;
                    db.Entry(data).State = System.Data.Entity.EntityState.Modified;
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
