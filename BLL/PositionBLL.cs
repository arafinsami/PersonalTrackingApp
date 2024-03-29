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
    public class PositionBLL
    {
        public static void save(POSITION position)
        {
            PositionDAO.save(position);
        }

        public static List<PositionDTO> findAll()
        {
            return PositionDAO.findAll();
        }

        public static void update(POSITION position)
        {
            PositionDAO.update(position);
        }

        public static void delete(int positionID)
        {
            PositionDAO.delete(positionID);
        }
    }
}
