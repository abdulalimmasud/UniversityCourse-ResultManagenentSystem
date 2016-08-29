using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystem.Models;
using UniversityCourseResultManagementSystem.UniversityCRM.Core.DAL;

namespace UniversityCourseResultManagementSystem.UniversityCRM.Core.BLL
{
    public class ResultManager
    {
        ResultGatway resultGatway = new ResultGatway();

        public List<StudentResult> GetResult(int id)
        {
            return resultGatway.GetResultById(id);
        }
        public bool IsResultSave(StudentResult result)
        {
            int count = resultGatway.IsResultSave(result);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool SaveResult(StudentResult result)
        {
            int id = resultGatway.SaveResult(result);
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Grade> GetGradeLetter()
        {
            return resultGatway.GetGradeLetter();
        }
    }
}