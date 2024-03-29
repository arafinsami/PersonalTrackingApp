﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DAO
{
    public class DepartmentDAO : EmployeeContext
    {
        public static void addDepartment(DEPARTMENT department)
        {
            try
            {
                context.DEPARTMENTs.InsertOnSubmit(department);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void delete(int departmentID)
        {
            try
            {
                DEPARTMENT d = context.DEPARTMENTs.First(x => x.ID == departmentID);
                context.DEPARTMENTs.DeleteOnSubmit(d);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static List<DEPARTMENT> findAll()
        {
            List<DEPARTMENT> departments = context.DEPARTMENTs.ToList();
            return departments;
        }

        public static void update(DEPARTMENT department)
        {
            try
            {
                DEPARTMENT d = context.DEPARTMENTs.First(x => x.ID == department.ID);
                d.DepartmentName = department.DepartmentName;
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
