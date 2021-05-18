using HomeRentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeRentManagementSystem.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            DatabaseEntities dc = new DatabaseEntities();
            string Emailid = User.Identity.Name;
            var acc = dc.RegisterUsers.FirstOrDefault(s => s.Email.Equals(Emailid));
            List<RegisterUser> register = new List<RegisterUser>();
            register.Add(acc);
            return View();
        }
        public ActionResult MyProfile()
        {
            DatabaseEntities dc = new DatabaseEntities();
            string Emailid = User.Identity.Name;
            var acc = dc.RegisterUsers.FirstOrDefault(s => s.Email.Equals(Emailid));
            List<RegisterUser> register = new List<RegisterUser>();
            register.Add(acc);
            Session["Name"] = Convert.ToString(acc.FirstName + "" + acc.LastName);
            ViewBag.image = acc.ProfileImage;

            return View(register.AsEnumerable());
        }


        public ActionResult Edit(int? Id)
        {
            DatabaseEntities dc = new DatabaseEntities();
            var acc = dc.RegisterUsers.Where(s => s.Id == Id).FirstOrDefault();

            if(acc == null) 
            {
                return HttpNotFound();
            }
            return View(acc);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,Password,PhoneNumber,Aadharno,ResetPasswordCode")] RegisterSeller Registers, HttpPostedFileBase image)
        {
            DatabaseEntities dc = new DatabaseEntities();
            if (image != null)
            {
                Registers.ProfileImage = new byte[image.ContentLength];
                image.InputStream.Read(Registers.ProfileImage, 0, image.ContentLength);
            }
            if (ModelState.IsValid)

            {
                dc.Entry(Registers).State = EntityState.Modified;
                dc.SaveChanges();
                return RedirectToAction("MyProfile");
            }
            return View(Registers);
        }
        public ActionResult Viewhouse(string search)
        {
            DatabaseEntities dc = new DatabaseEntities();
            string Emailid = User.Identity.Name;
            var acc1 = dc.RegisterUsers.FirstOrDefault(s => s.Email.Equals(Emailid));
            ViewBag.image = acc1.ProfileImage;
            List<Property_Table> acc = new List<Property_Table>();
            //long rent = long.Parse(search);
            var acc2 = dc.RegisterUsers.FirstOrDefault(s => s.Email.Equals(Emailid));
            if (search != null || search == "")
            {
                return View(acc);
            }
            else
            {
                return View(acc);
            }

            
        }
        public ActionResult Bookings()
        {
            DatabaseEntities dc = new DatabaseEntities();
            string Emailid = User.Identity.Name;
            var acc1 = dc.RegisterUsers.FirstOrDefault(s => s.Email.Equals(Emailid));
            ViewBag.image = acc1.ProfileImage;
            List<Property_Table> acc = new List<Property_Table>();
            return View(acc);
            
        }
    }
}







