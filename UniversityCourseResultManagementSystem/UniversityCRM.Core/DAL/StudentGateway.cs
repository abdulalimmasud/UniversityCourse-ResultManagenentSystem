using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystem.Models;

namespace UniversityCourseResultManagementSystem.UniversityCRM.Core.DAL
{
    public class StudentGateway:BaseGateway
    {
        public int CheckEnrollCourse(CourseEnroll course)
        {
            string query = "select COUNT(CourseCode) as [count] from Management.EnrollCourse where StudentId =@id and CourseCode = @code";
            SqlCommand.CommandText = query;
            SqlCommand.Parameters.Clear();

            SqlCommand.Parameters.Add("@id", SqlDbType.Int);
            SqlCommand.Parameters["@id"].Value = course.Student;
            SqlCommand.Parameters.Add("@code", SqlDbType.VarChar);
            SqlCommand.Parameters["@code"].Value = course.Course;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count = int.Parse(reader["count"].ToString());
            }
            reader.Close();
            SqlConnection.Close();

            return count;
        }
        public int EnrollCourse(CourseEnroll course)
        {
            string query = "insert Management.EnrollCourse values(@id, @code, @date)";
            SqlCommand.CommandText = query;
            SqlCommand.Parameters.Clear();

            SqlCommand.Parameters.Add("@id", SqlDbType.Int);
            SqlCommand.Parameters["@id"].Value = course.Student;
            SqlCommand.Parameters.Add("@code", SqlDbType.NVarChar);
            SqlCommand.Parameters["@code"].Value = course.Course;
            SqlCommand.Parameters.Add("@date", SqlDbType.NVarChar);
            SqlCommand.Parameters["@date"].Value = course.Dates;

            SqlConnection.Open();
            int rowAffected = SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();
            return rowAffected;
        }
        public List<Student> GetAllStudentInfo()
        {
            SqlCommand.CommandText = "spStudentInfo";
            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            List<Student> studentInfoList = new List<Student>();
            while (reader.Read())
            {
                Student student = new Student();
                student.Id = int.Parse(reader["Id"].ToString());
                student.Name = reader["Name"].ToString();
                student.Email = reader["Email"].ToString();
                student.Department = reader["Department"].ToString();
                student.RegistrationNo = reader["RegistrationNo"].ToString();
                studentInfoList.Add(student);
            }
            reader.Close();
            SqlConnection.Close();
            return studentInfoList;
        }
        public int RegisterStudet(Student student)
        {
            string query = "insert Management.Student (Name,Email,ContactNo,AdmissionDate,StudentAddress,DeptCode) values(@name,@mail,@phone,@date,@address,@dept)";
            SqlCommand.CommandText = query;
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@name", SqlDbType.VarChar);
            SqlCommand.Parameters["@name"].Value = student.Name;
            SqlCommand.Parameters.Add("@mail", SqlDbType.NVarChar);
            SqlCommand.Parameters["@mail"].Value = student.Email;
            SqlCommand.Parameters.Add("@phone", SqlDbType.VarChar);
            SqlCommand.Parameters["@phone"].Value = student.ContactNo;
            SqlCommand.Parameters.Add("@date", SqlDbType.NVarChar);
            SqlCommand.Parameters["@date"].Value = student.Date;
            SqlCommand.Parameters.Add("@address", SqlDbType.NVarChar);
            SqlCommand.Parameters["@address"].Value = student.Address;
            SqlCommand.Parameters.Add("@dept", SqlDbType.NVarChar);
            SqlCommand.Parameters["@dept"].Value = student.Department;

            SqlConnection.Open();
            int rowAffected = SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();
            return rowAffected;
        }
        public int GetExistingStudentByMail(string email)
        {
            SqlCommand.CommandText = "select COUNT(Email) as [count] from Management.Student where Email = @mail";
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@mail", SqlDbType.NVarChar);
            SqlCommand.Parameters["@mail"].Value = email;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            int rowCount = 0;
            while (reader.Read())
            {
                rowCount = Convert.ToInt32(reader["count"].ToString());
            }
            reader.Close();
            SqlConnection.Close();

            return rowCount;
        }
        public int GetExistingStudentByPhone(string contact)
        {
            SqlCommand.CommandText = "select COUNT(ContactNo) as [count] from Management.Student where ContactNo = @phone";
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@phone", SqlDbType.VarChar);
            SqlCommand.Parameters["@phone"].Value = contact;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            int rowCount = 0;
            while (reader.Read())
            {
                rowCount = Convert.ToInt32(reader["count"].ToString());
            }
            reader.Close();
            SqlConnection.Close();

            return rowCount;
        }
        public string GetRegistrationNo(string phone)
        {
            SqlCommand.CommandText = "Select RegistrationNo from Management.Student where ContactNo = @phone";
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@phone", SqlDbType.VarChar);
            SqlCommand.Parameters["@phone"].Value = phone;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            string regNo = null;
            while (reader.Read())
            {
                regNo = reader["RegistrationNo"].ToString();
            }
            reader.Close();
            SqlConnection.Close();
            return regNo;
        }
    }
}