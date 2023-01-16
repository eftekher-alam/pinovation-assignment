using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using PinovationAssignment.Data;
using PinovationAssignment.Models;
using PinovationAssignment.ViewModel;
using System;
using System.Collections.Generic;

namespace PinovationAssignment.Controllers
{
    public class UserController : Controller
    {
        private readonly IApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IUtility _utility;

        public UserController(IApplicationDbContext context, IWebHostEnvironment env, IUtility utility)
        {
            _context = context;
            _env = env;
            _utility = utility;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            List<TblCity> cities = _context.GetAllCity();
            ViewBag.city = cities;
            List<TblUsers> users = _context.GetAllUsers();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<TblCountry> countries = _context.GetAllCountry();
            ViewBag.countries = countries;
            return View();
        }


        [HttpPost]
        public IActionResult Create(TblUserVM uservm)
        {
            List<TblCountry> countries = _context.GetAllCountry();
            ViewBag.countries = countries;

            List<TblCity> cities = _context.GetAllCity();
            ViewBag.city = cities;

            if (ModelState.IsValid)
            {
                TblUsers user = new TblUsers()
                {
                    fName = uservm.fName,
                    lName = uservm.lName,
                    phoneNo = uservm.phoneNo,
                    password = uservm.password,
                    dob = uservm.dob,
                    cityId = uservm.cityId
                };

                //Set user ID
                if (uservm.Gender == "Male")
                    user.userId = _utility.GetEvenId();
                else if (uservm.Gender == "Female")
                    user.userId = _utility.GetOddId();
                else
                    user.userId = _utility.GetPrimeId();

                //Check email unique
                if (_context.IsEmailUnique(uservm.emailNo))
                {
                    user.emailNo = uservm.emailNo;
                }
                else
                {
                    ModelState.AddModelError("emailNo", "Email already exist");
                    return View(uservm);
                }

                //Picture
                if (uservm.userImg != null)
                {
                    string rootPath = _env.WebRootPath;
                    string picPath = "Image\\" + Guid.NewGuid().ToString() + uservm.userImg.FileName.ToString();
                    string fullPath = Path.Combine(rootPath, picPath);
                    FileStream stream = new FileStream(fullPath, FileMode.Create);
                    uservm.userImg.CopyTo(stream);
                    stream.Close();
                    user.userImg = picPath;
                }

                //CV
                if (uservm.userCV != null)
                {
                    string extention = Path.GetExtension(uservm.userCV.FileName);
                    if (extention == ".xlsx" || extention == ".doc" || extention == ".pdf")
                    {
                        string rootPath = _env.WebRootPath;
                        string cvPath = "CV\\" + Guid.NewGuid().ToString() + uservm.userCV.FileName.ToString();
                        string fullPath = Path.Combine(rootPath, cvPath);
                        FileStream stream = new FileStream(fullPath, FileMode.Create);
                        uservm.userCV.CopyTo(stream);
                        stream.Close();
                        user.userCV = cvPath;
                    }
                    else
                    {
                        ModelState.AddModelError("userCV", "CV format must be pdf, doc or excel.");
                        ModelState.AddModelError("userImg", "You have to select image again.");
                        return View(uservm);
                    }

                }
                _context.InsertUser(user);
                return RedirectToAction("Details", "User", new { userId = user.userId });
            }
            return View(uservm);
        }



        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Edit(int userId)
        {
            TblUsers user = _context.GetUserById(userId);
            TblUserVM userVM = new TblUserVM()
            {
                userId = user.userId,
                fName = user.fName,
                lName = user.lName,
                Gender = (_utility.IsPrime(user.userId)) ? "Other" : (user.userId % 2 == 0) ? "Male" : "Female",
                phoneNo = user.phoneNo,
                emailNo = user.emailNo,
                password = user.password,
                dob = user.dob,
                cityId = user.cityId
            };


            List<TblCountry> countries = _context.GetAllCountry();
            ViewBag.countries = countries;

            List<TblCity> cities = _context.GetAllCity();
            ViewBag.city = cities;

            foreach (var item in cities)
            {
                if (user.cityId == item.cityId)
                {
                    userVM.countryId = item.countryId;
                    break;
                }
            }
            return View(userVM);
        }


        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult Edit(TblUserVM uservm)
        {
            TblUsers user = _context.GetUserById(uservm.userId);

            List<TblCountry> countries = _context.GetAllCountry();
            ViewBag.countries = countries;

            List<TblCity> cities = _context.GetAllCity();
            ViewBag.city = cities;

            if (ModelState.IsValid)
            {
                user.userId = uservm.userId;
                user.fName = uservm.fName;
                user.lName = uservm.lName;
                user.phoneNo = uservm.phoneNo;
                user.dob = uservm.dob;
                user.cityId = uservm.cityId;

                if(uservm.password != null)
                {
                    user.password = uservm.password;
                }

                //Picture
                if (uservm.userImg != null)
                {
                    string rootPath = _env.WebRootPath;
                    string picPath = "Image\\" + Guid.NewGuid().ToString() + uservm.userImg.FileName.ToString();
                    string fullPath = Path.Combine(rootPath, picPath);
                    FileStream stream = new FileStream(fullPath, FileMode.Create);
                    uservm.userImg.CopyTo(stream);
                    stream.Close();
                    user.userImg = picPath;
                }

                //CV
                if (uservm.userCV != null)
                {
                    string extention = Path.GetExtension(uservm.userCV.FileName);
                    if (extention == ".xlsx" || extention == ".doc" || extention == ".pdf")
                    {
                        string rootPath = _env.WebRootPath;
                        string cvPath = "CV\\" + Guid.NewGuid().ToString() + uservm.userCV.FileName.ToString();
                        string fullPath = Path.Combine(rootPath, cvPath);
                        FileStream stream = new FileStream(fullPath, FileMode.Create);
                        uservm.userCV.CopyTo(stream);
                        stream.Close();
                        user.userCV = cvPath;
                    }
                    else
                    {
                        ModelState.AddModelError("userCV", "CV format must be pdf, doc or excel.");
                        ModelState.AddModelError("userImg", "Please again select image.");
                        return View(uservm);
                    }

                }
                _context.UpdateUser(user);
                return RedirectToAction("Details", "User", new { userId = user.userId });
            }
            uservm.Gender = (_utility.IsPrime(uservm.userId)) ? "Other" : (uservm.userId % 2 == 0) ? "Male" : "Female";
            return View(uservm);
        }


        [Authorize(Roles = "User")]
        public IActionResult Details(int userId)
        {
            TblUsers user = _context.GetUserById(userId);
            ViewBag.Gender = (_utility.IsPrime(user.userId)) ? "Other" : (user.userId % 2 == 0) ? "Male" : "Female";
            List < TblCountry > countries = _context.GetAllCountry();
            ViewBag.countries = countries;

            List<TblCity> cities = _context.GetAllCity();
            ViewBag.city = cities;

            return View(user);
        }

        public IActionResult DownloadFile(string userCV)
        {
            string rootPath = _env.WebRootPath;
            string filePath = Path.Combine(rootPath, userCV);
            if (filePath == null) return NotFound();
            return PhysicalFile(filePath, MimeTypes.GetMimeType(filePath), Path.GetFileName(filePath));
        }

        public JsonResult GetCity(int countryId)
        {
            List<TblCity> cities = _context.GetCityByCountryId(countryId);
            return Json(new SelectList(cities, "cityId", "cityName"));
        }
    }
}
