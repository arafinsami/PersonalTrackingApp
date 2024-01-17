using DAO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DAO.DAO;

namespace BLL
{
    public class TaskBLL
    {
        public static TaskDTO findAllTasks()
        {
            TaskDTO dto = new TaskDTO();
            dto.employees = EmployeeDAO.findAllEmployee();
            dto.positions = PositionDAO.findAll();
            dto.departments = DepartmentDAO.findAll();
            dto.taskStates = TaskDAO.findAllTaskStates();
            dto.tasks = TaskDAO.findAllTasks();
            return dto;
        }

        public static void save(TASK task)
        {
            TaskDAO.save(task);
        }

        public static void update(TASK task)
        {
            TaskDAO.update(task);
        }
    }
}
