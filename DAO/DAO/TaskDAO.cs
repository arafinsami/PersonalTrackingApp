using DAO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAO.DAO
{
    public class TaskDAO : EmployeeContext
    {
        public static List<TaskDetailDTO> findAllTasks()
        {
            List<TaskDetailDTO> taskDetailDTOs = new List<TaskDetailDTO>();
            var list = (
                           from t in context.TASKs
                           join ts in context.TASKSTATEs on t.TaskState equals ts.ID
                           join e in context.EMPLOYEEs on t.EmployeeID equals e.ID
                           join d in context.DEPARTMENTs on t.DepartmentID equals d.ID
                           join p in context.POSITIONs on t.PositionID equals p.ID
                           select new
                           {
                               taskID = t.ID,
                               title = t.TaskTitle,
                               content = t.TaskContent,
                               taskStartDate = t.TaskStartDate,
                               taskDeliveryDate = t.TaskDeliveryDate,
                               taskStateName = ts.StateName,
                               taskStateID = t.TaskState,
                               employeeID = t.EmployeeID,
                               name = e.Name,
                               surName = e.SurName,
                               userNo = e.UserNo,
                               positionID = p.ID,
                               positionName = p.PositionName,
                               departmentID = d.ID,
                               departmentName = d.DepartmentName
                           }
                       ).ToList();

            foreach (var item in list)
            {
                TaskDetailDTO dto = new TaskDetailDTO();
                dto.taskID = item.taskID;
                dto.title = item.title;
                dto.content = item.content;
                dto.taskStartDate = item.taskStartDate;
                dto.taskDeliveryDate = item.taskDeliveryDate;
                dto.taskStateName = item.taskStateName;
                dto.taskStateID = item.taskStateID;
                dto.employeeID = item.employeeID;
                dto.name = item.name;
                dto.surName = item.surName;
                dto.userNo = item.userNo;
                dto.positionID = item.positionID;
                dto.positionName = item.positionName;
                dto.departmentID = item.departmentID;
                dto.departmentName = item.departmentName;
                taskDetailDTOs.Add(dto);
            }
            return taskDetailDTOs;
        }

        public static List<TASKSTATE> findAllTaskStates()
        {
            return context.TASKSTATEs.ToList();
        }

        public static void save(TASK task)
        {
            try
            {
                context.TASKs.InsertOnSubmit(task);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void update(TASK task)
        {
            try
            {
                TASK t = context.TASKs.First(x => x.ID == task.ID);
                t.TaskTitle = task.TaskTitle;
                t.TaskContent = task.TaskContent;
                t.EmployeeID = task.EmployeeID;
                t.TaskState = task.TaskState;
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void delete(int taskID)
        {
            try
            {
                TASK t = context.TASKs.First(x => x.ID == taskID);
                context.TASKs.DeleteOnSubmit(t);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
