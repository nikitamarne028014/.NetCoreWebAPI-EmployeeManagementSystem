using AutoMapper;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [Produces(MediaTypeNames.Application.Json , MediaTypeNames.Application.Xml)]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _EmployeeService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper , ILogger logger)
        {
            _EmployeeService = employeeService;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: Employee - Get List of Employees
        [HttpGet]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetAllEmployees(string sortBy)
        {
            _logger.Information("GetAllEmployees Flow Start");
            _logger.Information("Sort Employees using " + sortBy + " Order");
            return Ok(_EmployeeService.GetAllEmployees(sortBy));
        }

        // GET: Employee/1 - Get Employee By Id
        [HttpGet("{EmpId:int}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetEmployeeById(int EmpId)
        {
            var Employee = _EmployeeService.GetEmployeeById(EmpId);

            if (Employee == null)
                return NotFound(new { statusCode = StatusCodes.Status404NotFound, message = "Employee "+ EmpId +" Not Found" });

            var response = _mapper.Map<EmployeeViewModel>(Employee);

            return Ok(response);
        }

        // POST: EmployeeController/Create - Add a new Employee
        [HttpPost]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult CreateEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (employee == null)
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "Employee Should Not Be Null." });
 
            try
            {
                var result = _EmployeeService.CreateEmployee(employee);
                var response = _mapper.Map<EmployeeViewModel>(result);

                return CreatedAtAction(nameof(CreateEmployee), response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = ex.Message });
            }
        }

        // PUT: Employee/UpdateEmployee/5
        [HttpPut("{EmpId:int}")]
        [ProducesResponseType(typeof(Employee), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateEmployee(int EmpId, [FromBody] Employee employee)
        {
            if (employee == null)
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = "Employee Should Not Be Null." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = _EmployeeService.UpdateEmployee(EmpId, employee);
                var response = _mapper.Map<EmployeeViewModel>(result);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest , message = ex.Message} );
            } 
        }

        // DELETE: EmployeeController/Delete/5
        [HttpDelete("{EmpId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteEmployee(int EmpId)
        {
            _EmployeeService.DeleteEmployee(EmpId);
            return Ok(new { statusCode = StatusCodes.Status200OK, message = "Employee Deleted Successfully..!" });
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public ActionResult getPage(int pageNumber, int pageSize)
        {
            return Ok(_EmployeeService.PagingEmployeeObject(pageNumber, pageSize));
        }

        [HttpGet("{name}")]
        public ActionResult SearchEmployeeByName(string name)
        {
            return Ok(_EmployeeService.SearchEmployeeByName(name));
        }
    }
}
