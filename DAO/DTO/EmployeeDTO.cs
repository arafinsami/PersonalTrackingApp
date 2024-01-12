using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;


namespace DAO.DTO
{
    public class EmployeeDTO
    {
        public List<DEPARTMENT> departments { get; set; }
        public List<PositionDTO> positions { get; set; }
        public List<EmployeeDetailsDTO> employees { get; set; }
    }
}
