using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseResultManagementSystem.Models;
using UniversityCourseResultManagementSystem.UniversityCRM.Core.BLL;
using UniversityCourseResultManagementSystem.ViewModel;

namespace UniversityCourseResultManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        CourseManager courseManager = new CourseManager();
        SemesterManager semesterManager = new SemesterManager();
        // GET: Course
        public ActionResult SaveCourse()
        {
            ViewBag.DepartmentList = DeptList();
            ViewBag.Semesters = SemesterList();
            return View();
        }
        [HttpPost]
        public ActionResult SaveCourse([Bind(Include = "Code,Name,Credit,Description,Department,Semester")] Course course)
        {
            bool isSave = false;
            bool courseExist = false;
            // course.Code.Length >=5 && course.Credit>=0.5 && course.Credit <=5
            if(ModelState.IsValid)
            {
                courseExist = courseManager.IsCourseExists(course.Code);
                if (!courseExist)
                {
                    isSave = courseManager.SaveCourse(course);
                    if (isSave)
                    {
                        ViewBag.Message = "Successefully Saved";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to Save!!!!!";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Course exists in data";
                }
            }
            ViewBag.DepartmentList = DeptList();
            ViewBag.Semesters = SemesterList();
            return View();
        }

        public ActionResult CourseStatics()
        {
            ViewBag.DepartmentList = DeptList();
            return View();
        }

        public ActionResult UnAssignCourses(int? value)
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnAssignCourses()
        {
            bool isCheck = courseManager.CheckUnassign();
            bool isUnassign = false;
            if(isCheck == false)
            {
                isUnassign = courseManager.CourseUnassign();
                if (isUnassign)
                {
                    ViewBag.Message = "All courses unassign successfully";
                }
                else
                {
                    ViewBag.Message = "Something Error";
                }             
            }
            else
            {
                ViewBag.ErrorMessage = "Courses Already unassign"; 
            }
            return View();
        }
        public JsonResult ViewCourseStatics(string code)
        {
            List<CourseStatics> courses = courseManager.GetCourseStatics(code);
           
            return Json(courses, JsonRequestBehavior.AllowGet);
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

        private List<SelectListItem> SemesterList()
        {
            List<Semester> semesters = semesterManager.GetAllSemester();
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "------Select-----",Selected=true });
            foreach (Semester semester in semesters)
            {
                selectList.Add(new SelectListItem { Value = semester.Id.ToString(), Text = semester.SemesterName });
            }
            return selectList;
        }
    }
}