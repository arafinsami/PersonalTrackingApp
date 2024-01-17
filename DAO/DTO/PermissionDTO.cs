using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DTO
{
    public class PermissionDTO
    {
        public List<PermissionDetailsDTO> permissions { get; set; }
        public List<DEPARTMENT> departments { get; set; }
        public List<PositionDTO> positions { get; set; }
        public List<PERMISSIONSTATE> permissionStates { get; set; }
    }
}
