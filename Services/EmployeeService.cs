using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeManagementDBContext _dBContext;

        public EmployeeService(EmployeeManagementDBContext employeeManagementDBContext)
        {
            _dBContext = employeeManagementDBContext;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _dBContext.Employee.Find(employeeId);
        }

        public List<Employee> GetAllEmployees(string sortBy)
        {
            IQueryable<Employee> employees_in_db;
            List<Employee> employees = new List<Employee>();

            if (sortBy != null)
                sortBy = sortBy.Trim().ToLower();

            switch(sortBy)
            {
                case "asc":
                    employees_in_db = _dBContext.Employee.OrderByDescending(q => q.CreatedAt);
                break;

                case "desc":
                    employees_in_db = _dBContext.Employee.OrderBy(q => q.CreatedAt);
                break;
                    
                default:
                    employees_in_db = _dBContext.Employee;
                break;
            }

            foreach(var employee in employees_in_db)
            {
                employees.Add(employee);
            }

            return employees;
        }

        public Employee CreateEmployee(Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            employee.CreatedAt = DateTime.UtcNow;

            var entityEntry = _dBContext.Employee.Add(employee);

            _dBContext.SaveChanges();

            return entityEntry.Entity;
        }

        public Employee UpdateEmployee(int employeeId, Employee employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));

            var search_emp = GetEmployeeById(employeeId);

            if (search_emp == null)
                throw new Exception("Entity Not Found In Database");

            search_emp.EmployeeName = employee.EmployeeName;
            search_emp.EmployeeSalary = employee.EmployeeSalary;
            search_emp.EmployeeEmail = employee.EmployeeEmail;
            search_emp.EmployeeAddress = employee.EmployeeAddress;
            search_emp.EmployeeDesignation = employee.EmployeeDesignation;

            _dBContext.SaveChanges();

            return search_emp;
        }

        public void DeleteEmployee(int employeeId)
        {
            var search_emp = GetEmployeeById(employeeId);

            if (search_emp == null)
                throw new Exception("Entity Not Found In Database");

            _dBContext.Employee.Remove(search_emp);

            _dBContext.SaveChanges();
        }

        public IQueryable<Employee> PagingEmployeeObject(int pageNumber, int pageSize)
        {
            var employees = _dBContext.Employee.OrderBy(e => e.EmployeeId);
            var paged_value = employees.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return paged_value;
        }

        public IQueryable<Employee> SearchEmployeeByName(string name)
        {
            var employees = _dBContext.Employee.Where(q => q.EmployeeName.Contains(name));
            return employees;
        }

    }
}

