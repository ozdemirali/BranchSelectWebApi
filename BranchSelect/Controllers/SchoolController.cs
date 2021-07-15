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
        /// This method save school infpormation on School Table from Database
        /// </summary>
        /// <param name="setup"></param>
        /// <returns>Ok</returns>
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
                Student data = new Student();



                for (var i = 1; i < result.Tables[0].Rows.Count; i++)
                {
                    data.Id = result.Tables[0].Rows[i][0].ToString();
                    data.NameAndSurname = result.Tables[0].Rows[i][1].ToString();
                    data.Class= result.Tables[0].Rows[i][2].ToString();
                    data.Score = float.Parse(result.Tables[0].Rows[1][3].ToString());
               

                    using (var db = new BranchSelectDbContext())
                    {
                        if (db.Students.Where(s=>s.Id==data.Id).Count()>0)
                        {
                            db.Entry(data).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            db.Entry(data).State = System.Data.Entity.EntityState.Added;

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
