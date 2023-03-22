using FluentValidation;

namespace SalesAndInventory.Api.Models
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(20).WithMessage("Last name must be no longer than 20 characters.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(10).WithMessage("First name must be no longer than 10 characters.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(30).WithMessage("Title must be no longer than 30 characters.");

            RuleFor(x => x.TitleOfCourtesy)
                .NotEmpty().WithMessage("Title of courtesy is required.")
                .MaximumLength(25).WithMessage("Title of courtesy must be no longer than 25 characters.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Birth date is required.")
                .Must(x => x <= DateTime.Now.Date).WithMessage("Birth date cannot be greater than today's date.");

            RuleFor(x => x.HireDate)
                .NotEmpty().WithMessage("Hire date is required.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(60).WithMessage("Address must be no longer than 60 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(15).WithMessage("City must be no longer than 15 characters.");

            RuleFor(x => x.Region)
                .MaximumLength(15).WithMessage("Region must be no longer than 15 characters.");

            RuleFor(x => x.PostalCode)
                .MaximumLength(10).WithMessage("Postal code must be no longer than 10 characters.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(15).WithMessage("Country must be no longer than 15 characters.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .MaximumLength(24).WithMessage("Phone must be no longer than 24 characters.");

            RuleFor(x => x.ManagerId)
                .Must((emp, mgrid) => mgrid == null || mgrid != emp.EmpId).WithMessage("ManagerId cannot be the same as EmpId.");

            RuleFor(x => x.Subordinates)
                .Must((employee, subordinates) => subordinates == null || !subordinates.Any(s => s.EmpId == employee.EmpId))
                .WithMessage("An employee cannot be their own subordinate.");
        }
    }
}