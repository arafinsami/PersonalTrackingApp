﻿using DAO.DAO;
using DAO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAO
{
    public class EmployeeDAO : EmployeeContext
    {
        public static List<EmployeeDetailsDTO> findAllEmployee()
        {
            List<EmployeeDetailsDTO> dto = new List<EmployeeDetailsDTO>();
            var list = (
                         from e in context.EMPLOYEEs
                         join d in context.DEPARTMENTs on e.DepartmentID equals d.ID
                         join p in context.POSITIONs on e.PositionID equals p.ID
                         select new
                         {
                             employeeID = e.ID,
                             userNo = e.UserNo,
                             password = e.Password,
                             name = e.Name,
                             surName = e.SurName,
                             departmentID = e.DepartmentID,
                             departmentName = d.DepartmentName,
                             positionID = e.PositionID,
                             positionName = p.PositionName,
                             salary = e.Salary,
                             address = e.Address,
                             isAdmin = e.isAdmin,
                             imagePath = e.ImagePath,
                             birthDate = e.BirthDay
                         }
                       ).OrderBy(x => x.userNo).ToList();
            foreach (var item in list)
            {
                EmployeeDetailsDTO employeeDetailsDTO = new EmployeeDetailsDTO();
                employeeDetailsDTO.employeeID = item.employeeID;
                employeeDetailsDTO.userNo = item.userNo;
                employeeDetailsDTO.password = item.password;
                employeeDetailsDTO.name = item.name;
                employeeDetailsDTO.surName = item.surName;
                employeeDetailsDTO.departmentID = item.departmentID;
                employeeDetailsDTO.departmentName = item.departmentName;
                employeeDetailsDTO.positionID = item.positionID;
                employeeDetailsDTO.positionName = item.positionName;
                employeeDetailsDTO.salary = item.salary;
                employeeDetailsDTO.address = item.address;
                employeeDetailsDTO.isAdmin = item.isAdmin;
                employeeDetailsDTO.imagePath = item.imagePath;
                employeeDetailsDTO.birthDate = item.birthDate;
                dto.Add(employeeDetailsDTO);
            }
            return dto;
        }

        public static List<EMPLOYEE> findByUserNoAndPassword(int userNo, string password)
        {
            List<EMPLOYEE> employees = context.EMPLOYEEs
                .Where(e => e.UserNo == userNo
                && e.Password == password).ToList();
            return employees;
        }

        public static List<EMPLOYEE> isUserNoUnique(int userNo)
        {
            return context.EMPLOYEEs.Where(x => x.UserNo == userNo).ToList();
        }

        public static void save(EMPLOYEE employee)
        {
            try
            {
                context.EMPLOYEEs.InsertOnSubmit(employee);
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void update(int employeeID, int salaryAmount)
        {
            try
            {
                EMPLOYEE employee = context.EMPLOYEEs.First(x => x.ID == employeeID);
                employee.Salary = salaryAmount;
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void update(EMPLOYEE employee)
        {
            try
            {
                EMPLOYEE e = context.EMPLOYEEs.First(x => x.ID == employee.ID);
                e.UserNo = employee.UserNo;
                e.Name = employee.Name;
                e.SurName = employee.SurName;
                e.Password = employee.Password;
                e.isAdmin = employee.isAdmin;
                e.BirthDay = employee.BirthDay;
                e.Address = employee.Address;
                e.DepartmentID = employee.DepartmentID;
                e.PositionID = employee.PositionID;
                e.Salary = employee.Salary;
                e.ImagePath = employee.ImagePath;
                context.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
