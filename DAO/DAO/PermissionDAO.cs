using DAO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DAO
{
    public class PermissionDAO : EmployeeContext
    {
        public static void delete(int permissionID)
        {
            try
            {
                PERMISSION p = context.PERMISSIONs.First(x => x.ID == permissionID);
                context.PERMISSIONs.DeleteOnSubmit(p);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static List<PermissionDetailsDTO> findAllPermission()
        {
            List<PermissionDetailsDTO> permissions = new List<PermissionDetailsDTO>();
            var list = (
                        from p in context.PERMISSIONs
                        join ps in context.PERMISSIONSTATEs on p.PermissionState equals ps.ID
                        join e in context.EMPLOYEEs on p.EmployeeID equals e.ID
                        select new
                        {
                            employeeID = e.ID,
                            userNo = e.UserNo,
                            name = e.Name,
                            surName = e.SurName,
                            stateName = ps.StateName,
                            stateID = ps.ID,
                            startDate = p.PermissionStartDate,
                            endDate = p.PermissionEndDate,
                            permissionID = p.ID,
                            explanation = p.PermissionExplanation,
                            permissionDayAmount = p.PermissionDay,
                            departmentID = e.DepartmentID,
                            positionID = e.PositionID,
                            PermissionID = p.ID
                        }
                       ).OrderBy(x => x.startDate).ToList();
            foreach (var item in list)
            {
                PermissionDetailsDTO dto = new PermissionDetailsDTO();
                dto.employeeID = item.employeeID;
                dto.userNo = item.userNo;
                dto.name = item.name;
                dto.surName = item.surName;
                dto.permissionDayAmount = item.permissionDayAmount;
                dto.startDate = item.startDate;
                dto.endDate = item.endDate;
                dto.departmentID = item.departmentID;
                dto.positionID = item.positionID;
                dto.stateID = item.stateID;
                dto.stateName = item.stateName;
                dto.explanation = item.explanation;
                dto.permissionID = item.PermissionID;
                permissions.Add(dto);
            }
            return permissions;
        }

        public static List<PERMISSIONSTATE> findAllPermissionState()
        {
            List<PERMISSIONSTATE> permissionStates = context.PERMISSIONSTATEs.ToList();
            return permissionStates;

        }

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

        public static void update(PERMISSION permission)
        {
            try
            {
                PERMISSION pr = context.PERMISSIONs.First(x => x.ID == permission.ID);
                pr.PermissionStartDate = permission.PermissionStartDate;
                pr.PermissionEndDate = permission.PermissionEndDate;
                pr.PermissionExplanation = permission.PermissionExplanation;
                pr.PermissionDay = permission.PermissionDay;
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void update(int permissionID, int permissionStates)
        {
            try
            {
                PERMISSION pr = context.PERMISSIONs.First(x => x.ID == permissionID);
                pr.PermissionState = permissionStates;
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
