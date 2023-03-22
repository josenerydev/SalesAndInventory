using FluentValidation;

namespace SalesAndInventory.Api.Models
{
    public class SupplierValidator : AbstractValidator<Supplier>
    {
        public SupplierValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Company Name is required")
                .Length(1, 40).WithMessage("Company Name must be between 1 and 40 characters");

            RuleFor(x => x.ContactName)
                .NotEmpty().WithMessage("Contact Name is required")
                .Length(1, 30).WithMessage("Contact Name must be between 1 and 30 characters");

            RuleFor(x => x.ContactTitle)
                .NotEmpty().WithMessage("Contact Title is required")
                .Length(1, 30).WithMessage("Contact Title must be between 1 and 30 characters");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .Length(1, 60).WithMessage("Address must be between 1 and 60 characters");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required")
                .Length(1, 15).WithMessage("City must be between 1 and 15 characters");

            RuleFor(x => x.Region)
                .Length(0, 15).WithMessage("Region must be between 0 and 15 characters");

            RuleFor(x => x.PostalCode)
                .Length(0, 10).WithMessage("Postal Code must be between 0 and 10 characters");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required")
                .Length(1, 15).WithMessage("Country must be between 1 and 15 characters");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required")
                .Length(1, 24).WithMessage("Phone must be between 1 and 24 characters");

            RuleFor(x => x.Fax)
                .Length(0, 24).WithMessage("Fax must be between 0 and 24 characters");
        }
    }
}