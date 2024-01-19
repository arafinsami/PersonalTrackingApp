using DAO.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DAO
{
    public class PositionDAO : EmployeeContext
    {
        public static void save(POSITION position)
        {
            try
            {
                context.POSITIONs.InsertOnSubmit(position);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static List<PositionDTO> findAll()
        {
            var positions = (from p in context.POSITIONs
                             join d in context.DEPARTMENTs
                             on p.DepartmentID equals d.ID
                             select new
                             {
                                 positionID = p.ID,
                                 positionName = p.PositionName,
                                 departmentName = d.DepartmentName,
                                 departmentID = p.DepartmentID
                             }
                        ).OrderBy(x => x.positionID).ToList();
            List<PositionDTO> positionDTOs = new List<PositionDTO>();
            foreach (var position in positions)
            {
                PositionDTO dto = new PositionDTO();
                dto.ID = position.positionID;
                dto.PositionName = position.positionName;
                dto.DepartmentID = position.departmentID;
                dto.DepartmentName = position.departmentName;
                positionDTOs.Add(dto);
            }
            return positionDTOs;
        }

        public static void update(POSITION position)
        {
            try
            {
                POSITION p = context.POSITIONs.First(x => x.ID == position.ID);
                p.PositionName = position.PositionName;
                p.DepartmentID = position.DepartmentID;
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void delete(int positionID)
        {
            try
            {
                POSITION p = context.POSITIONs.First(x => x.ID == positionID);
                context.POSITIONs.DeleteOnSubmit(p);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
