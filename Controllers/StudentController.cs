using CRUDwithADO.Data;
using CRUDwithADO.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace CRUDwithADO.Controllers
{
    public class StudentController : Controller
 {
        private readonly DataRep dataRep;

        public StudentController()
        {
            dataRep = new DataRep();
        }
        public IActionResult Index()
        {
            var data = dataRep.RetriveData();

            //var StudentsList = new List<Student>();

            //foreach (var index in data)
            //{
            //    var student = new Student
            //    {
            //        Id = index.Id,
            //        Name = index.Name,
            //        Age = index.Age,
            //        Address = index.Address,
            //        Gender = index.Gender,
            //        Status = index.Status,
            //        Education = index.Education
            //    };
            //    StudentsList.Add(student);
            //}
            return View(data);
        }


        public IActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Insert(Student student)
        {

                dataRep.InsertData(student);
               
                return RedirectToAction(nameof(Index));
                // return View(student);
        }

        public IActionResult Update(int id)
        {
            var student = dataRep.GetStudentById(id);  
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Update(Student student)
        {

            if (ModelState.IsValid)
            {
                dataRep.UpdateData(student);
                //Console.WriteLine(nameof(Index));
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public IActionResult Delete(int id)
        {
            var student = dataRep.GetStudentById(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {

            dataRep.DeleteData(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var student = dataRep.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
    }
}
