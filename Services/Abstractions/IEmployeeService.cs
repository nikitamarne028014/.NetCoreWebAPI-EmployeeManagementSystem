using EmployeeManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services.Abstractions
{
    public interface IEmployeeService
    {
        public List<Employee> GetAllEmployees(string sortBy);
        public Employee GetEmployeeById(int employeeId);
        public Employee CreateEmployee(Employee employee);
        public Employee UpdateEmployee(int employeeId, Employee employee);
        public void DeleteEmployee(int employeeId);
        public IQueryable<Employee> PagingEmployeeObject(int pageNumber, int pageSize);
        public IQueryable<Employee> SearchEmployeeByName(string name);

    }
}
