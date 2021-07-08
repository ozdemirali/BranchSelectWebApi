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
    }
}
