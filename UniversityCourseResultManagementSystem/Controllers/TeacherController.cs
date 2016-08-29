using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseResultManagementSystem.Models;
using UniversityCourseResultManagementSystem.UniversityCRM.Core.BLL;

namespace UniversityCourseResultManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        TeacherManager teacherManager = new TeacherManager();
        DepartmentManager departmetnManager = new DepartmentManager();
        // GET: Teacher
        public ActionResult SaveTeacher()
        {
            ViewBag.DesigList = DesignationList();
            ViewBag.DeptList = DepartmentList();
            return View();
        }
        [HttpPost]
        public ActionResult SaveTeacher([Bind(Include = "Name,Address,Email,ContactNo,DesignationId,DepartmentCode,Credit")]Teacher teacher)
        {
            bool isSave = false;
            bool isExists = false;
            if (ModelState.IsValid)
            {
                isExists = teacherManager.IsTeacherExist(teacher);
                if (!isExists)
                {
                    isSave = teacherManager.SaveTeacher(teacher);
                    if (isSave)
                    {
                        ViewBag.Message = "Save Successfully";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to Save";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Teacher already exists";
                    ModelState.Clear();
                }
            }
            ViewBag.DesigList = DesignationList();
            ViewBag.DeptList = DepartmentList();
            return View();
        }
        private List<SelectListItem> DesignationList()
        {
            List<Designation> designationList = teacherManager.GetTeacherDesignation();
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "---------Select----------", Selected=true});
            foreach (Designation designation in designationList)
            {
                selectList.Add(new SelectListItem { Value = designation.Id.ToString(), Text = designation.Name });
            }
            return selectList;
        }

        private List<SelectListItem> DepartmentList()
        {
            List<Department> departmentList = departmetnManager.GetAllDepartment();
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "---------------------Select---------------------",Selected=true });
            foreach (Department department in departmentList)
            {
                selectList.Add(new SelectListItem { Value = department.Code, Text = department.Name });
            }
            return selectList;
        }
    }
}