using DAO;
using DAO.DAO;
using DAO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EmployeeBLL
    {
        public static EmployeeDTO findAllWithPositionsAndDepartments()
        {
            EmployeeDTO dto = new EmployeeDTO();
            dto.departments = DepartmentDAO.findAll();
            dto.positions = PositionDAO.findAll();
            dto.employees = EmployeeDAO.findAllEmployee();
            return dto;
        }

        public static bool isUserNoUnique(int userNo)
        {
            List<EMPLOYEE> employees = EmployeeDAO.isUserNoUnique(userNo);
            if (employees.Count > 0)
                return false;
            else
                return true;
        }

        public static void save(EMPLOYEE employee)
        {
            EmployeeDAO.save(employee);
        }
    }
}
