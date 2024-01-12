using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DTO
{
    public class EmployeeDetailsDTO
    {
        public int employeeID {  get; set; }
        public int userNo {  get; set; }
        public string password { get; set; }
        public string name {  get; set; }
        public string surName {  get; set; }
        public int departmentID {  get; set; }
        public string departmentName {  get; set; }
        public int positionID {  get; set; }
        public string positionName { get; set; }
        public int salary { get; set; }
        public string address { get; set; }
        public bool isAdmin { get; set; }
        public string imagePath { get; set; }
        public DateTime? birthDate { get; set; }
    }
}
