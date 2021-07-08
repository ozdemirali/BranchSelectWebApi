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
    public class StudentController : ApiController
    {
        private BranchSelectDbContext db;

        /// <summary>
        /// This Method get data from Studut Table by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                                        Confirmation=s.Confirmation,

                                    }).Where(s=>s.Id==id).FirstOrDefault();

                    //string s = JsonConvert.SerializeObject(student, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    //var student = new StudentView();
                    //student.Id = data.Id;
                    //student.NameAndSurname = data.NameAndSurname;
                    //student.Class = data.Class;
                    //student.ParentNameAndSurname = data.ParentNameAndSurname;
                    //student.Adress = data.Adress;
                    //student.Phone = data.Phone;
                    //student.Email = data.Email;
                    return Ok(student);

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
                    data.Confirmation = student.Confirmation;
                    db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Ok();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
