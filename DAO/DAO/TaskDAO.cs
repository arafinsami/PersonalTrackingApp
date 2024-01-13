using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DAO
{
    public class TaskDAO : EmployeeContext
    {
        public static List<TASKSTATE> findAllTasks()
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
    }
}
