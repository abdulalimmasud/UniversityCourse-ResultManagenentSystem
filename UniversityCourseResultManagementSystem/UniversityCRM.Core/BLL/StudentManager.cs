using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystem.Models;
using UniversityCourseResultManagementSystem.UniversityCRM.Core.DAL;

namespace UniversityCourseResultManagementSystem.UniversityCRM.Core.BLL
{
    public class StudentManager
    {
        StudentGateway studentGetway = new StudentGateway();
        public bool IsCourseEnrolled(CourseEnroll course)
        {
            int count = studentGetway.CheckEnrollCourse(course);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsEnrollCourse(CourseEnroll course)
        {
            return studentGetway.EnrollCourse(course) > 0;
        }
        public Student GetBasicInfo(int id)
        {
            List<Student> allInfo = studentGetway.GetAllStudentInfo();
            //Student student = allInfo.Where(x=>x.Id==id).Select(x=>x.Name,)
            var info = (from sInfo in allInfo where sInfo.Id == id select new { Name = sInfo.Name, Email = sInfo.Email, dept = sInfo.Department }).FirstOrDefault();
            Student student = new Student(); 
            //foreach (var stdnt in info)
            //{
                student = new Student();
                student.Name = info.Name;
                student.Email = info.Email;
                student.Department = info.dept;
            //}
            return student;
        }
        public List<Student> GetAllStudentInfo()
        {
            List<Student> studentInfoList = studentGetway.GetAllStudentInfo();
            //List<Student> studentRegList = new List<Student>();
            //var infos = from sInfo in studentInfoList select new { id = sInfo.Id, regNo = sInfo.RegistrationNo };
            //foreach (var info in infos)
            //{
            //    Student student = new Student();
            //    student.Id = info.id;
            //    student.RegistrationNo = info.regNo;
            //    studentRegList.Add(student);
            //}
            //return studentRegList;
            return studentInfoList;
        }
        public bool RegisterStudent(Student student)
        {
            return studentGetway.RegisterStudet(student) > 0;
        }
        public bool IsStudentExists(Student student)
        {
            int mailCount = studentGetway.GetExistingStudentByMail(student.Email);
            int phoneCount = studentGetway.GetExistingStudentByPhone(student.ContactNo);
            if (mailCount > 0)
            {
                return true;
            }
            else if (phoneCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string GetRegistrationNo(string phone)
        {
            return studentGetway.GetRegistrationNo(phone);
        }
    }
}