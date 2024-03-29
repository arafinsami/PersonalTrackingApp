﻿using DAO;
using DAO.DAO;
using DAO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PermissionBLL
    {
        public static void delete(int permissionID)
        {
            PermissionDAO.delete(permissionID);
        }

        public static PermissionDTO findAllPermission()
        {
            PermissionDTO dto = new PermissionDTO();
            dto.permissions = PermissionDAO.findAllPermission();
            dto.departments = DepartmentDAO.findAll();
            dto.positions = PositionDAO.findAll();
            dto.permissionStates = PermissionDAO.findAllPermissionState();
            return dto;
        }

        public static void save(PERMISSION permission)
        {
            PermissionDAO.save(permission);
        }

        public static void update(PERMISSION permission)
        {
            PermissionDAO.update(permission);
        }

        public static void update(int permissionID, int permissionStates)
        {
            PermissionDAO.update(permissionID, permissionStates);
        }
    }
}
