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
    public class DepartmentController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        RoomManager roomManager = new RoomManager();
        CourseManager courseManager = new CourseManager();
        // GET: Department
        public ActionResult SaveDepartment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveDepartment(Department department)
        {
            bool isSave = false;
            //department.Name != null && department.Code.Length >=2 && department.Code.Length <=7
            if(ModelState.IsValid)
            {
                if(!departmentManager.IsDeparmentExist(department))
                {
                    isSave = departmentManager.Save(department);
                    if(isSave)
                    {
                        ViewBag.Message = "Successfully Saved";
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Something Error";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Department Code Already Exists";
                }
            }
            else
            {
                ViewBag.Message = "Requried field is empty or Code is not correct";
            }

            return View();
        }

        public ActionResult DepartmentList()
        {
            List<Department> departmentList =  departmentManager.GetAllDepartment();
            return View(departmentList);
        }

        public ActionResult ClassRoom()
        {
            ViewBag.DepartmentList = DeptList();
            ViewBag.RoomList = RoomList();
            ViewBag.DayList = DayList();
            return View();
        }
        [HttpPost]
        public ActionResult ClassRoom([Bind(Include = "Department,Course,RoomNo,DayName,Form,StartPeriod,To,EndPeriod")]RoomAllocation room)
        {
            bool isAllocate = false;            
            bool checkOverlap = false;
            bool IsEndTimeGreater = roomManager.CheckEndTimeGreater(room);
            if (ModelState.IsValid)
            {
                if (IsEndTimeGreater)
                {
                    checkOverlap = roomManager.CheckTime(room);
                    if (!checkOverlap)
                    {
                        if (ModelState.IsValid)
                        {
                            isAllocate = roomManager.IsAllocate(room);
                            if (isAllocate)
                            {
                                ViewBag.Message = "Room Allocation Successful";
                            }
                            else
                            {
                                ViewBag.ErrorMessage = "Allocation Failed";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Classroom is engaged between this period";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "End time must be greater than Start Time";
                }
            }
            
            ViewBag.DepartmentList = DeptList();
            ViewBag.RoomList = RoomList();
            ViewBag.DayList = DayList();
            return View();
        }

        public ActionResult ClassSchedule()
        {
            ViewBag.DepartmentList = DeptList();
            return View();
        }

        public ActionResult UnallocateClassrooms(int? value)
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnallocateClassrooms()
        {
            bool isAlreadyUnallocte = roomManager.CheckUnallocateRooms();
            bool isUnallocate = false;
            if (!isAlreadyUnallocte)
            {
                isUnallocate = roomManager.UnallocateClassrooms();
                if (isUnallocate)
                {
                    ViewBag.Message = "All Rooms unallocate successfully";
                }
                else
                {
                    ViewBag.ErrorMessage = "Something error";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Allrooms already unallocated";
            }
            
            return View();
        }
        public JsonResult ViewClassSchedule(string code)
        {
            List<ClassSchedule> schedule = roomManager.GetClassSchedule(code);

            return Json(schedule, JsonRequestBehavior.AllowGet);
        }
        private List<SelectListItem> DeptList()
        {
            List<Department> departmentList = departmentManager.GetAllDepartment();
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "---------------------Select---------------------", Selected = true });
            foreach (Department department in departmentList)
            {
                selectList.Add(new SelectListItem { Value = department.Code, Text = department.Name });
            }
            return selectList;
        }

        private List<SelectListItem> RoomList()
        {
            List<Room> roomList = roomManager.GetAllRoom();
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "-----------------Select----------------",Selected=true });
            foreach (Room room in roomList)
            {
                selectList.Add(new SelectListItem { Value = room.Id.ToString(), Text = room.RoomNo });
            }
            return selectList;
        }

        private List<SelectListItem> DayList()
        {
            List<Day> dayList = roomManager.GetAllDay();
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "-----------------Select----------------", Selected = true });
            foreach (Day day in dayList)
            {
                selectList.Add(new SelectListItem { Value = day.Id.ToString(), Text = day.Name });
            }
            return selectList;
        }

        public JsonResult CourseList(string code)
        {
            List<Course> courses = courseManager.GetCourseOfDepartment(code);
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "", Text = "--------------Select-------------", Selected = true });
            foreach (Course course in courses)
            {
                selectList.Add(new SelectListItem { Value = course.Code, Text = course.Name });
            }

            return Json(new SelectList(selectList, "Value", "Text"));
        }
    }
}