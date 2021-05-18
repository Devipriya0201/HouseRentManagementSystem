using HomeRentManagementSystem.Models;
using HomeRentManagementSystem.ViewModel;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
using System.Web.Helpers;
using System.Data.Entity;
using System.Drawing;
using System.IO;




namespace HomeRentManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MyProfile()
        {

            DatabaseEntities dc = new DatabaseEntities();
            string Emailid = User.Identity.Name;
            var acc = dc.RegisterSellers.FirstOrDefault(s => s.Email.Equals(Emailid));
            List<RegisterSeller> register = new List<RegisterSeller>();
            register.Add(acc);
            Session["Name"] = Convert.ToString(acc.FirstName + "" + acc.LastName);
            ViewBag.image = acc.ProfileImage;

            return View(register.AsEnumerable());

        }
        public ActionResult Edit(int? Id)
        {
            DatabaseEntities dc = new DatabaseEntities();
            var acc = dc.RegisterSellers.Where(s => s.Id == Id).FirstOrDefault();

            if (acc == null)
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
        public ActionResult AddHouse()
        {
            return View();
        }



        public ActionResult Bookings(string button, int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string buttonClicked = Request.Form["SubmitButton"];
            if (buttonClicked == "Accept")
            {
                DatabaseEntities dc = new DatabaseEntities();

                //DatabaseEntities databaseEntities = db.DatabaseEntities.Find(id);
                dc.AppStatus = "APPROVED";
                dc.SaveChanges();

            }
            else if (buttonClicked == "Decline")
            {
                DatabaseEntities dc = new DatabaseEntities();
                dc.AppStatus = "UNAPPROVED";
                dc.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        public ActionResult CustomerDetails()
        {

            DatabaseEntities dc = new DatabaseEntities();
            string Emailid = User.Identity.Name;
            var acc1 = dc.RegisterSellers.FirstOrDefault(s => s.Email.Equals(Emailid));
            var acc = dc.RegisterSellers.Where(s => s.Email == Emailid).ToList();
            return View(acc);


        }
        public ActionResult ViewHouse()
        {

            DatabaseEntities dc = new DatabaseEntities();
            string Emailid = User.Identity.Name;
            var acc1 = dc.RegisterSellers.FirstOrDefault(s => s.Email.Equals(Emailid));
            var acc = dc.Property_Tables.Where(s => s.Email == Emailid).ToList();
            return View(acc);

        }



        public ActionResult SavePropertyDetails(PropertyModel registerDetails, HttpPostedFileBase images)
        {
            if (ModelState.IsValid)
            {
                using (var databaseContext = new DatabaseEntities())
                {
                    Property_Table reglog = new Property_Table();
                    var userWithSameEmail = databaseContext.Property_Tables.Where(m => m.Sellername == "Izas").FirstOrDefault();

                    if (userWithSameEmail == null)
                    {
                        reglog.Sellername = registerDetails.Sellername;
                        reglog.Houseno = registerDetails.Houseno;
                        reglog.Streetname = registerDetails.Streetname;
                        reglog.Address = registerDetails.Address;
                        reglog.City = registerDetails.City;
                        reglog.Zip = registerDetails.Zip;
                        reglog.Flattype = registerDetails.Flattype;
                        reglog.Rent = registerDetails.Rent;
                        reglog.Phone = registerDetails.Phone;
                        reglog.Email = registerDetails.Email;
                        reglog.Description = registerDetails.Description;


                        if (images != null)
                        {
                            reglog.Images = new byte[images.ContentLength];
                            images.InputStream.Read(reglog.Images, 0, images.ContentLength);

                        }
                        try
                        {
                            databaseContext.Property_Tables.Add(reglog);
                            databaseContext.SaveChanges();


                            ViewBag.saved = "Registration Successful..!";
                            return RedirectToAction("Index");
                        }
                        catch (NullReferenceException ex)
                        {
                            ViewBag.Message = "Something Wrong Please Try Again";
                            return View("AddHouse");
                        }

                    }
                    else
                    {

                        ViewBag.Message = "Email Already Exists";

                        return View("AddHouse");
                    }
                }
            }
            else
            {
                return View("AddHouse", registerDetails);
            }
        }


        [HttpPost]
        public ActionResult SaveRegisterDetails1(Register registerDetails)
        {
            //We check if the model state is valid or not. We have used DataAnnotation attributes.
            //If any form value fails the DataAnnotation validation the model state becomes invalid.
            if (ModelState.IsValid)
            {
                //create database context using Entity framework
                using (var databaseContext = new DatabaseEntities())
                {

                    //If the model state is valid i.e. the form values passed the validation then we are storing the User's details in DB.
                    RegisterSeller reglog = new RegisterSeller();
                    var userWithSameEmail = databaseContext.RegisterSellers.Where(m => m.Email == registerDetails.Email).FirstOrDefault(); //checking if the emailid already exits for any user
                    //var user = databaseContext.RegisterUsers.Where(query => query.Email.Equals(registerDetails.Email)).SingleOrDefault();
                    //If user is present, then true is returned.
                    //Save all details in RegitserUser object
                    if (userWithSameEmail == null)
                    {
                        reglog.FirstName = registerDetails.FirstName;
                        reglog.LastName = registerDetails.LastName;
                        reglog.Email = registerDetails.Email;
                        reglog.PhoneNumber = registerDetails.PhoneNumber;
                        reglog.Password = registerDetails.Password;
                        reglog.Aadharno = registerDetails.Aadharno;
                        try
                        {
                            databaseContext.RegisterSellers.Add(reglog);
                            databaseContext.SaveChanges();


                            ViewBag.saved = "Registration Successful!";
                            return RedirectToAction("Login");
                        }
                        catch (NullReferenceException ex)
                        {
                            ViewBag.Message = "Somthing Wrong Please Try Again";
                            return View("Register");
                        }

                        TempData["ErrorMessage"] = "Registration Is Success";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Email Already Exists";

                        return View("Register");
                    }
                }

            }
            else
            {

                //If the validation fails, we are returning the model object with errors to the view, which will display the error messages.
                return View("Register", registerDetails);
            }
        }
    }
}
    

        