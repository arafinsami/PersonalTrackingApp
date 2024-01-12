using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DAO
{
    public class DepartmentDAO : EmployeeContext
    {
        public static void addDepartment(DEPARTMENT department)
        {
            try
            {
                context.DEPARTMENTs.InsertOnSubmit(department);
                context.SubmitChanges();
            } 
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static List<DEPARTMENT> findAll()
        {
            List<DEPARTMENT> departments = context.DEPARTMENTs.ToList();
            return departments;
        }
    }
}
