using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Models
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        #region Constructors

        public EmployeeValidator()
        {
            RuleFor(x => x.EmployeeName)
                .NotEmpty()
                .MinimumLength(1).WithMessage("Minimum length is 1 char")
                .MaximumLength(40).WithMessage("Maximum length is 40 char");

            RuleFor(x => x.EmployeeEmail)
                .MaximumLength(254).WithMessage("Maximum length is 254 char")
                .EmailAddress()
                .NotEmpty()
                .MaximumLength(254).WithMessage("Maximum length is 254 char");

            RuleFor(x => x.EmployeeDesignation)
                .MaximumLength(100).WithMessage("Maximum Length can be 100 char")
                .NotEmpty().WithMessage("Employee Designation is Required");

            RuleFor(x => x.DepartmentId)
                .NotNull().WithMessage("DepartmentId is required")
                .InclusiveBetween(1, 10).WithMessage("DepartmentId Inclusive between 1 to 10");

            RuleFor(x => x.EmployeeSalary)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(5000);
        }
        #endregion
    }
}
