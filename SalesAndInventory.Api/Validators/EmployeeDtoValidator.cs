using FluentValidation;
using SalesAndInventory.Api.Dtos;

namespace SalesAndInventory.Api.Validators
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator()
        {
            RuleFor(x => x.LastName)
                .NotNull().WithMessage("Last name is required.")
                .NotEmpty().WithMessage("Last name cannot be empty.")
                .MaximumLength(20).WithMessage("Last name must be at most 20 characters long.");

            RuleFor(x => x.FirstName)
                .NotNull().WithMessage("First name is required.")
                .NotEmpty().WithMessage("First name cannot be empty.")
                .MaximumLength(10).WithMessage("First name must be at most 10 characters long.");

            RuleFor(x => x.Title)
                .NotNull().WithMessage("Title is required.")
                .NotEmpty().WithMessage("Title cannot be empty.")
                .MaximumLength(30).WithMessage("Title must be at most 30 characters long.");

            RuleFor(x => x.TitleOfCourtesy)
                .NotNull().WithMessage("Title of courtesy is required.")
                .NotEmpty().WithMessage("Title of courtesy cannot be empty.")
                .MaximumLength(25).WithMessage("Title of courtesy must be at most 25 characters long.");

            RuleFor(x => x.BirthDate)
                .NotNull().WithMessage("Birth date is required.")
                .LessThanOrEqualTo(System.DateTime.Now).WithMessage("Birth date cannot be in the future.");

            RuleFor(x => x.HireDate)
                .NotNull().WithMessage("Hire date is required.")
                .LessThanOrEqualTo(System.DateTime.Now).WithMessage("Hire date cannot be in the future.");

            RuleFor(x => x.Address)
                .NotNull().WithMessage("Address is required.")
                .NotEmpty().WithMessage("Address cannot be empty.")
                .MaximumLength(60).WithMessage("Address must be at most 60 characters long.");

            RuleFor(x => x.City)
                .NotNull().WithMessage("City is required.")
                .NotEmpty().WithMessage("City cannot be empty.")
                .MaximumLength(15).WithMessage("City must be at most 15 characters long.");

            RuleFor(x => x.Region)
                .MaximumLength(15).WithMessage("Region must be at most 15 characters long.");

            RuleFor(x => x.PostalCode)
                .MaximumLength(10).WithMessage("Postal code must be at most 10 characters long.");

            RuleFor(x => x.Country)
                .NotNull().WithMessage("Country is required.")
                .NotEmpty().WithMessage("Country cannot be empty.")
                .MaximumLength(15).WithMessage("Country must be at most 15 characters long.");

            RuleFor(x => x.Phone)
                .NotNull().WithMessage("Phone is required.")
                .NotEmpty().WithMessage("Phone cannot be empty.")
                .MaximumLength(24).WithMessage("Phone must be at most 24 characters long.");
        }
    }
}