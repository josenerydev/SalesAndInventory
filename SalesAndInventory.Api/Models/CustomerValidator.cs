using FluentValidation;

namespace SalesAndInventory.Api.Models
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.CompanyName)
                .NotEmpty().WithMessage("Company name is required.")
                .MaximumLength(40).WithMessage("Company name cannot exceed 40 characters.");

            RuleFor(c => c.ContactName)
                .NotEmpty().WithMessage("Contact name is required.")
                .MaximumLength(30).WithMessage("Contact name cannot exceed 30 characters.");

            RuleFor(c => c.ContactTitle)
                .NotEmpty().WithMessage("Contact title is required.")
                .MaximumLength(30).WithMessage("Contact title cannot exceed 30 characters.");

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(60).WithMessage("Address cannot exceed 60 characters.");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(15).WithMessage("City cannot exceed 15 characters.");

            RuleFor(c => c.Region)
                .MaximumLength(15).WithMessage("Region cannot exceed 15 characters.");

            RuleFor(c => c.PostalCode)
                .MaximumLength(10).WithMessage("Postal code cannot exceed 10 characters.");

            RuleFor(c => c.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(15).WithMessage("Country cannot exceed 15 characters.");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .MaximumLength(24).WithMessage("Phone cannot exceed 24 characters.");

            RuleFor(c => c.Fax)
                .MaximumLength(24).WithMessage("Fax cannot exceed 24 characters.");
        }
    }
}