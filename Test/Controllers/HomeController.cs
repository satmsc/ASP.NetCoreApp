using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Test.Models;




namespace Test.Controllers
{
  //  [Route("Home")]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepositary _emprep;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(IEmployeeRepositary emprepoitory,IHostingEnvironment hostingEnvironment)
        {
            _emprep = emprepoitory;
            this.hostingEnvironment = hostingEnvironment;
        }

        //[Route("")]
        //[Route("Index")]
        //[Route("~/")]
        [AllowAnonymous]
        public ViewResult Index()
        {
           IEnumerable<Employee> emp = _emprep.GetAllEmployees();
            return View(emp);
        }

        //   [Route("details/{id?}")]
        [AllowAnonymous]
        public ViewResult details(int? id)
        {
           // throw new Exception("Excepion in details");

            Employee empl= _emprep.GetEmployee(id.Value);
            if(empl== null)
            {
                Response.StatusCode = 404;
                return View("ErrorPage", id.Value);
            }

            Employee emp = empl;
            ViewData["page"] = "Employee";
            ViewData["EmpModal"] = emp;
            return View(emp);
        }
        [HttpGet]
        public ViewResult Create()
        {

            return View();
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee emp = _emprep.GetEmployee(id);
            EmployeeEditModalView nemp = new EmployeeEditModalView()
            {
                Id=emp.Id,
                Name=emp.Name,
                Department=emp.Department,
                ExistingImagePath=emp.FilePath
            };
            return View(nemp);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeEditModalView modal)
        {
            if (ModelState.IsValid)
            {
                Employee emp = _emprep.GetEmployee(modal.Id);
                emp.Name = modal.Name;
                emp.Department = modal.Department;
                
                if(modal.Photo!=null)
                {
                    string path =Path.Combine(hostingEnvironment.WebRootPath, "Images", modal.ExistingImagePath);
                    System.IO.File.Delete(path);
                    emp.FilePath= GetUniqueFilePath(modal);
                }

                Employee nemp = _emprep.Update(emp);
                return RedirectToAction("Index");
            }
            return View();
        }

        private string GetUniqueFilePath(EmployeeCreateModalView modal)
        {
            string UniqFilePath = null;
            if (modal.Photo != null)
            {
                string UploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "Images");
                UniqFilePath = Guid.NewGuid().ToString() + "_" + modal.Photo.FileName;
                string filePath = Path.Combine(UploadFolder, UniqFilePath);
                using (var filePanthNew = new FileStream(filePath, FileMode.Create))
                {
                    modal.Photo.CopyTo(filePanthNew);
                }
                   
            }

            return UniqFilePath;
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateModalView modal)
        {
            if (ModelState.IsValid)
            {
                string UniqFilePath = GetUniqueFilePath(modal);
                Employee emp = new Employee()
                {
                    Name = modal.Name,
                    Department = modal.Department,
                    FilePath = UniqFilePath
                };

                Employee nemp = _emprep.Add(emp);
                return RedirectToAction("details", new { nemp.Id });
            }
            return View();
        } 
    }
}
