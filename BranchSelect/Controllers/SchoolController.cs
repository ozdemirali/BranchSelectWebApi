using BranchSelect.Context;
using BranchSelect.Models;
using BranchSelect.ViewModels;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BranchSelect.Controllers
{
   
    public class SchoolController : ApiController
    {
        //private BranchSelectDbContext db;
        /// <summary>
        /// This method get school information from School Table and Branch Table  
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles="Admin")]
        public IHttpActionResult Get()
        {
            try
            {
                using (var db = new BranchSelectDbContext())
                {
                    var data = new SetupViewModel();
                    if (db.Schools.Count() > 0)
                    {
                        var setupData = db.Schools.FirstOrDefault();
                        data.SchoolName = setupData.Name;
                        data.BranchTeacher = setupData.BranchTeacher;
                        data.AssistantDirector = setupData.AssistantDirector;
                        data.MinClassCount = setupData.MinClassCount;

                        var branchData = db.Branches.ToList();
                        data.FirstBranch = branchData[0].Name;
                        data.SecondBranch = branchData[1].Name;
                        

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
        /// This method save school information on School Table from Database
        /// </summary>
        /// <param name="setup"></param>
        /// <returns>Ok</returns>

        [Authorize(Roles = "Admin")]
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
                    dataSchool.AssistantDirector = setup.AssistantDirector;
                    dataSchool.MinClassCount = setup.MinClassCount;

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
        /// This method find role of user 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        [Route("api/School/GetRole")]
        public IHttpActionResult GetRole(String id)
        {
            try
            {
                using (var db=new BranchSelectDbContext())
                {
                    var role = (from u in db.Users
                                join r in db.Roles
                                on u.RoleId equals r.Id
                                where u.Id == id
                                select new
                                {   id=u.Id,
                                    role=r.Name
                                }
                              ).FirstOrDefault();
                    return Ok(role);
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
        /// This method save student information from excel file to Student table from Database
        /// </summary>
        /// <returns>Ok</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/School/Upload")]
        public async Task<string> Upload()
        {
            try
            {
                var provider = new MultipartMemoryStreamProvider();
                 await Request.Content.ReadAsMultipartAsync(provider);

                IExcelDataReader excelReader = null;
                
                // extract file name and file contents
                Stream stream= new MemoryStream(await provider.Contents[0].ReadAsByteArrayAsync());

                //get fileName
                var filename = provider.Contents[0].Headers.ContentDisposition.FileName.Replace("\"", string.Empty);

                //Check file type 
                if (filename.EndsWith(".xls"))
                {
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                }

                else if (filename.EndsWith(".xlsx"))
                {
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                else
                {
                    return "The File is not Excel File";
                }

                DataSet result = excelReader.AsDataSet();
                Student student = new Student();
                User user = new User();


                for (var i = 1; i < result.Tables[0].Rows.Count; i++)
                {

                    student.Id = result.Tables[0].Rows[i][0].ToString();
                    student.NameAndSurname = result.Tables[0].Rows[i][1].ToString();
                    student.Class= result.Tables[0].Rows[i][2].ToString();
                    student.Score = float.Parse(result.Tables[0].Rows[1][3].ToString());
                   
                    user.Id = result.Tables[0].Rows[i][0].ToString(); 
                    user.Password = user.Id.Remove(6);
                    user.RoleId=2;


                    using (var db = new BranchSelectDbContext())
                    {
                        //Checks if there is a recor in the student table
                        if (db.Students.Where(s=>s.Id== student.Id).Count()>0)
                        {
                            db.Entry(student).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            db.Entry(student).State = System.Data.Entity.EntityState.Added;

                        }

                        //Checks if there is a recor in the user table
                        if (db.Users.Where(u=>u.Id==user.Id).Count()>0)
                        {
                            db.Entry(user).State= System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            db.Entry(user).State = System.Data.Entity.EntityState.Added;
                        }
                        db.SaveChanges();
                    }

                }

                return "Ok";

            }
            catch (Exception e)
            {
                using (var db=new BranchSelectDbContext())
                {
                    var error = new Error();
                    error.Message = e.Message;
                    db.Errors.Add(error);
                    db.SaveChanges();
                }
                return e.Message.ToString();
            }
        }

    }
}
