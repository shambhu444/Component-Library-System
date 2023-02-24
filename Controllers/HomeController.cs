using Application_CLS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Application_CLS.Controllers
{
    public class HomeController : Controller
    {
        DBEntities db = new DBEntities();

        //Home : Index
        public ActionResult Index()
        {
            return View();
        }

        //Home : List
        public ActionResult List()
        {
            var data = db.Component_Library_Store.ToList();
            return View(data);
        }

        //Status
        [HttpPost]
        public ActionResult UpdateStatus(int SNo, string status)
        {
            // Retrieve the record from the database using the ID
            var record = db.Component_Library_Store.Find(SNo);
            // Update the status of the record
            record.Status = status;     
            // Save changes to the database
            db.SaveChanges();     
            // Redirect the user back to the list view
            return RedirectToAction("List");
        }


        //Home : Download File
        public FileResult Download(string fileName)
        {
            fileName = Path.Combine(Server.MapPath("~/Files/"), fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fileName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        //Home : Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Component_Library_Store s)
        {
            string fileName = Path.GetFileNameWithoutExtension(s.ImageFile.FileName);
            string extension = Path.GetExtension(s.ImageFile.FileName);
            HttpPostedFileBase postedFile = s.ImageFile;
            int length = postedFile.ContentLength;

            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".pdf" || extension.ToLower() == ".docx" || extension.ToLower() == ".txt" || extension.ToLower() == ".xlsx")
            {
                if (length <= 10000000)
                {
                    fileName = fileName + extension;
                    s.Upload_File = fileName;
                    fileName = Path.Combine(Server.MapPath("~/Files/"), fileName);
                    s.ImageFile.SaveAs(fileName);
                    db.Component_Library_Store.Add(s);

                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        ViewBag.Message = "<script>alert('Data Inserted Successfully..!')</script>";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = "<script>alert('Data Not Inserted !!')</script>";
                    }
                }
                else
                {
                    ViewBag.Message = "<script>alert('Size Should be of 10 MB !!')</script>";
                }
            }
            else
            {
                ViewBag.Message = "<script>alert('File Not Supported !!')</script>";
            }
            return View();
        }

        //Home : Lead
        public ActionResult Lead()
        {
            return View();
        }

        //Home : Login
        public ActionResult Login(Application_CLS.Models.empinfo avm, string ReturnUrl = "")
        {
            using (DBEntities db = new DBEntities())
            {
                    var ad = db.empinfoes.Where(x => x.Username == avm.Username && x.Password == avm.Password).FirstOrDefault();
                if (ad != null)
                {
                    FormsAuthentication.SetAuthCookie(ad.EmployeeName, false);
                    TempData["EmpRollno"] = ad.EmpRollno.ToString();
                    TempData["EmployeeName"] = ad.EmployeeName;
                    TempData["Designation"] = ad.Designation;

                    if (ad.Designation == "Lead")
                    {
                        return RedirectToAction("Lead", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Create", "Home");
                    }

                }
                else
                {
                   return View("Login", avm);
                }
            }
        }
    }
}
       


