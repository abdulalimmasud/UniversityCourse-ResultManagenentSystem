using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseResultManagementSystem.Models;
using UniversityCourseResultManagementSystem.UniversityCRM.Core.BLL;

namespace UniversityCourseResultManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        DepartmentManager departmentManager = new DepartmentManager();
        StudentManager studentManager = new StudentManager();
        CourseManager courseManager = new CourseManager();
        public ActionResult Register()
        {
            ViewBag.DepartmentList = DeptList();
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "Name,Email,ContactNo,Date,Address,Department")]Student student)
        {
            bool isSave = false;
            bool isExists = false;
            if (ModelState.IsValid)
            {
                isExists = studentManager.IsStudentExists(student);
                if (!isExists)
                {
                    isSave = studentManager.RegisterStudent(student);
                    if (isSave)
                    {
                        string regNo = studentManager.GetRegistrationNo(student.ContactNo);
                        ViewBag.Message = "Registration Successfull. Your Registration No is " + regNo;
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Registration Failed";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Student already registered";
                }
            }
            ViewBag.DepartmentList = DeptList();
            return View();
        }

        public JsonResult StudentInformation(int id)
        {
            var studentInfo = studentManager.GetBasicInfo(id);
            return Json(studentInfo, JsonRequestBehavior.AllowGet);
        }
        private List<SelectListItem> DeptList()
        {
            List<Department> departmentList = departmentManager.GetAllDepartment();
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "-----------------Select--------------------",Selected=true });
            foreach (Department department in departmentList)
            {
                selectList.Add(new SelectListItem { Value = department.Code, Text = department.Name });
            }
            return selectList;
        }
    }
}