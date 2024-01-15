using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DTO
{
    public class SalaryDTO
    {
        public List<SalaryDetailsDTO> salaries {  get; set; }
        public List<EmployeeDetailsDTO> employees { get; set; }
        public List<DEPARTMENT> departments { get; set; }
        public List<PositionDTO> positions { get; set; }
        public List<MONTH> months { get; set; }
    }
}
