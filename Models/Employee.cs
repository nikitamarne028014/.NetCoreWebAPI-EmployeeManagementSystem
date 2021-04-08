using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeAddress { get; set; }
        public string EmployeeDesignation { get; set; }
        public double EmployeeSalary { get; set; }
        public int DepartmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Department Department { get; set; }

    }
}
