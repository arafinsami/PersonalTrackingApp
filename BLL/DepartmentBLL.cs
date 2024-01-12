using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DAO.DAO;

namespace BLL
{
    public class DepartmentBLL
    {
        public static void addDepartment(DEPARTMENT department)
        {
            DepartmentDAO.addDepartment(department);
        }

        public static List<DEPARTMENT> findAll()
        {
            return DepartmentDAO.findAll();
        }
    }
}
