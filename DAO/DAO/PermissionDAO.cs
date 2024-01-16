using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DAO
{
    public class PermissionDAO : EmployeeContext
    {
        public static void save(PERMISSION permission)
        {
            try
            {
                context.PERMISSIONs.InsertOnSubmit(permission);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
