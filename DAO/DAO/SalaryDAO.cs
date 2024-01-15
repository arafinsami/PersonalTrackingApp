using DAO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAO.DAO
{
    public class SalaryDAO : EmployeeContext
    {
        public static List<MONTH> findAllMonths()
        {
            List<MONTH> months = context.MONTHs.ToList();
            return months;
        }

        public static List<SalaryDetailsDTO> findAllSalaries()
        {
            List<SalaryDetailsDTO> salaries = new List<SalaryDetailsDTO>();
            var list = (
                        from s in context.SALARies
                        join e in context.EMPLOYEEs on s.EmployeeID equals e.ID
                        join d in context.DEPARTMENTs on s.DepartmentID equals d.ID
                        join p in context.POSITIONs on s.PositionID equals p.ID
                        join m in context.MONTHs on s.MonthID equals m.ID
                        select new
                        {
                            employeeID = s.EmployeeID,
                            userNo = e.UserNo,
                            name = e.Name,
                            surName = e.SurName,
                            departmentID = s.DepartmentID,
                            departmentName = d.DepartmentName,
                            positionID = s.PositionID,
                            positionName = p.PositionName,
                            monthName = m.MonthName,
                            monthID = s.MonthID,
                            salaryAmount = s.Amount,
                            salaryYear = s.Year,
                            salaryID = s.ID
                        }
                       ).ToList();
            foreach (var item in list)
            {
                SalaryDetailsDTO dto = new SalaryDetailsDTO();
                dto.employeeID = item.employeeID;
                dto.userNo = item.userNo;
                dto.name = item.name;
                dto.surName = item.surName;
                dto.departmentID = item.departmentID;
                dto.departmentName = item.departmentName;
                dto.positionID = item.positionID;
                dto.positionName = item.positionName;
                dto.monthName = item.monthName;
                dto.monthID = item.monthID;
                dto.salaryAmount = item.salaryAmount;
                dto.salaryYear = item.salaryYear;
                dto.salaryID = item.salaryID;
                salaries.Add(dto);
            }
            return salaries;
        }

        public static void save(SALARY salary)
        {
            try
            {
                context.SALARies.InsertOnSubmit(salary);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
