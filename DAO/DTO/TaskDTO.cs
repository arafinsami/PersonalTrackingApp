using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DTO
{
    public class TaskDTO
    {
        public List<EmployeeDetailsDTO> employees {  get; set; }
        public List<DEPARTMENT> departments { get; set; }
        public List<PositionDTO> positions { get; set; }
        public List<TASKSTATE> taskStates { get; set; }
        public List<TaskDetailDTO> tasks { get; set; }
    }
}
