using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using UniversityCourseResultManagementSystem.Models;

namespace UniversityCourseResultManagementSystem.UniversityCRM.Core.DAL
{
    public class DepartmentGateway:BaseGateway
    {
        public int Save(Department department)
        {
            string query = "insert Management.Department values (@code, @name)";
            SqlCommand.CommandText = query;

            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@code", SqlDbType.NVarChar);
            SqlCommand.Parameters["@code"].Value = department.Code;

            SqlCommand.Parameters.Add("@name", SqlDbType.NVarChar);
            SqlCommand.Parameters["@name"].Value = department.Name;

            SqlConnection.Open();
            int rowAffected = SqlCommand.ExecuteNonQuery();
            SqlConnection.Close();

            return rowAffected;
        }
        public int GetNoOfDepartmentByCode(string code)
        {
            string query = "select COUNT(Code) as [count] from Management.Department where Code = @code";
            SqlCommand.CommandText = query;

            SqlCommand.Parameters.Clear();
            SqlCommand.Parameters.Add("@code", SqlDbType.NVarChar);
            SqlCommand.Parameters["@code"].Value = code;

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

        public List<Department> GetAllDepartment()
        {
            string query = "select * from Management.Department";
            SqlCommand.CommandText = query;

            SqlConnection.Open();
            SqlDataReader reader = SqlCommand.ExecuteReader();
            List<Department> departmentList = new List<Department>();
            while (reader.Read())
            {
                Department department = new Department();
                department.Code = reader["Code"].ToString();
                department.Name = reader["Name"].ToString();
                departmentList.Add(department);
            }
            reader.Close();
            SqlConnection.Close();

            return departmentList;
        }
    }
}