using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystem.UniversityCRM.Core.DAL;
using UniversityCourseResultManagementSystem.Models;

namespace UniversityCourseResultManagementSystem.UniversityCRM.Core.BLL
{
    public class DepartmentManager
    {
        DepartmentGateway departmentGetway = new DepartmentGateway();

        public bool Save(Department department)
        {
            return departmentGetway.Save(department) > 0;
        }
        public List<Department> GetAllDepartment()
        {
            return departmentGetway.GetAllDepartment();
        }
        public bool IsDeparmentExist(Department department)
        {
            int countDepartment = departmentGetway.GetNoOfDepartmentByCode(department.Code);
            if(countDepartment != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}