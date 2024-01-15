using DAO;
using DAO.DAO;
using DAO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;

namespace BLL
{
    public class SalaryBLL
    {
        public static SalaryDTO findAll()
        {
            SalaryDTO dto = new SalaryDTO();
            dto.employees = EmployeeDAO.findAllEmployee();
            dto.departments = DepartmentDAO.findAll();
            dto.positions = PositionDAO.findAll();
            dto.months = SalaryDAO.findAllMonths();
            dto.salaries = SalaryDAO.findAllSalaries();
            return dto;
        }

        public static void save(SALARY salary)
        {
            SalaryDAO.save(salary);
        }
    }
}
