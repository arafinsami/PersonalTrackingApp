using DAO;
using DAO.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PermissionBLL
    {
        public static void save(PERMISSION permission)
        {
            PermissionDAO.save(permission);
        }
    }
}
