using DAO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DAO
{
    public class SalaryDAO : EmployeeContext
    {
        public static List<MONTH> findAllMonths()
        {
            List<MONTH> months = context.MONTHs.ToList();
            return months;
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
