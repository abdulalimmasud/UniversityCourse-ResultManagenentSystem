using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystem.Models;
using UniversityCourseResultManagementSystem.UniversityCRM.Core.DAL;

namespace UniversityCourseResultManagementSystem.UniversityCRM.Core.BLL
{
    public class SemesterManager
    {
        SemesterGateway semsterGetway = new SemesterGateway();

        public List<Semester> GetAllSemester()
        {
            return semsterGetway.GetAllSemester();
        }
    }
}