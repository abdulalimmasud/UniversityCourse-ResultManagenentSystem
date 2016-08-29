using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystem.Models;
using UniversityCourseResultManagementSystem.ViewModel;

namespace UniversityCourseResultManagementSystem.UniversityCRM.Core.DAL
{
    public class TeacherGateway:BaseGateway
    {
        public int GetExistingTeacherByMail(string email)
        {
            SqlCommand.CommandText = "select COUNT(Email) as [count] from Management.Teacher where Email = @mail";
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
        public int GetExistingTeacherByPhone(string contact)
        {
            SqlCommand.CommandText = "select COUNT(Contact) as [count] from Management.Teacher where Contact = @phone";
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
        public double GetTeacherCredit(int id)
        {
            SqlCommand.CommandText = "Select * from Management.Teacher where Id= @id";
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@id", SqlDbType.Int);
            SqlCommand.Parameters["@id"].Value = id;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            //double teacher = 0;
            double teacherCredit = 0;
            while (reader.Read())
            {
                teacherCredit = double.Parse(reader["Credit"].ToString());
            }
            reader.Close();
            SqlConnection.Close();
            return teacherCredit;
        }
        public double GetTeacherAssignCredit(int id)
        {
            SqlCommand.CommandText = "spAssignCredit @id";
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@id", SqlDbType.Int);
            SqlCommand.Parameters["@id"].Value = id;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            //double teacher = 0;
            double teacherCredit = 0;
            while (reader.Read())
            {
                teacherCredit = double.Parse(reader["AssignCredit"].ToString());
            }
            reader.Close();
            SqlConnection.Close();
            return teacherCredit;
        }
        //public Teacher GetTeacherRemainingCredit(int id)
        //{
        //    SqlCommand.CommandText = "spGetTeacherCredit @id";
        //    SqlCommand.Parameters.Clear();
        //    SqlCommand.Parameters.Add("@id", SqlDbType.Int);
        //    SqlCommand.Parameters["@id"].Value = id;

        //    SqlConnection.Open();
        //    SqlDataReader reader = SqlCommand.ExecuteReader();
        //    //double teacherCredit = 0;
        //    Teacher teacherCredit = new Teacher();
        //    while (reader.Read())
        //    {
        //        teacherCredit.Credit = double.Parse(reader["Credit"].ToString());
        //        teacherCredit.Remaining = Convert.ToDouble(reader["Remaining"].ToString());                
        //    }
        //    reader.Close();
        //    SqlConnection.Close();

        //    return teacherCredit;
        //}
        public List<Teacher> GetTeacherByDepartment(string DeptCode)
        {
            SqlCommand.CommandText = "spTeacherOfDepartment @dCode";
            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@dCode", SqlDbType.NVarChar);
            SqlCommand.Parameters["@dCode"].Value = DeptCode;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();

            List<Teacher> teachers = new List<Teacher>();
            while(reader.Read())
            {
                Teacher teacher = new Teacher();
                teacher.Id = int.Parse(reader["Id"].ToString());
                teacher.Name = reader["Name"].ToString();
                teachers.Add(teacher);
            }
            reader.Close();
            SqlConnection.Close();
            return teachers;
        }
        public int SaveTeacher(Teacher teacher)
        {
            string query = "insert Management.Teacher (Name, Addres, Email, Contact, Designation, DeptCode, Credit) Values (@name, @address, @email, @contact, @designation, @dept, @credit)";
            SqlCommand.CommandText = query;

            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@name", SqlDbType.VarChar);
            SqlCommand.Parameters["@name"].Value = teacher.Name;

            SqlCommand.Parameters.Add("@address", SqlDbType.NVarChar);
            SqlCommand.Parameters["@address"].Value = teacher.Address;

            SqlCommand.Parameters.Add("@email", SqlDbType.NVarChar);
            SqlCommand.Parameters["@email"].Value = teacher.Email;

            SqlCommand.Parameters.Add("@contact", SqlDbType.VarChar);
            SqlCommand.Parameters["@contact"].Value = teacher.ContactNo;

            SqlCommand.Parameters.Add("@designation", SqlDbType.Int);
            SqlCommand.Parameters["@designation"].Value = teacher.DesignationId;

            SqlCommand.Parameters.Add("@dept", SqlDbType.NVarChar);
            SqlCommand.Parameters["@dept"].Value = teacher.DepartmentCode;

            SqlCommand.Parameters.Add("@credit", SqlDbType.Decimal);
            SqlCommand.Parameters["@credit"].Value = teacher.Credit;

            SqlConnection.Open();
            int rowAffected = SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();

            return rowAffected;
        }
        public List<Designation> GetTeacherDesignation()
        {
            string query = "select * from Management.Designation";
            SqlCommand.CommandText = query;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();

            List<Designation> designationList = new List<Designation>();
            while (reader.Read())
            {
                Designation designation = new Designation();
                designation.Id = int.Parse(reader["Id"].ToString());
                designation.Name = reader["Name"].ToString();

                designationList.Add(designation);
            }
            reader.Close();
            SqlConnection.Close();

            return designationList;
        }
    }
}