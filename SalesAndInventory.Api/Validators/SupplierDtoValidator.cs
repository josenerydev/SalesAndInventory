using FluentValidation;
using SalesAndInventory.Api.Dtos;

namespace SalesAndInventory.Api.Validators
{
    public class SupplierDtoValidator : AbstractValidator<SupplierDto>
    {
        public SupplierDtoValidator()
        {
            RuleFor(s => s.SupplierId)
                .GreaterThanOrEqualTo(0);

            RuleFor(s => s.CompanyName)
                .NotEmpty().WithMessage("CompanyName is required.")
                .Length(1, 40).WithMessage("CompanyName must be between 1 and 40 characters.");

            RuleFor(s => s.ContactName)
                .NotEmpty().WithMessage("ContactName is required.")
                .Length(1, 30).WithMessage("ContactName must be between 1 and 30 characters.");

            RuleFor(s => s.ContactTitle)
                .NotEmpty().WithMessage("ContactTitle is required.")
                .Length(1, 30).WithMessage("ContactTitle must be between 1 and 30 characters.");

            RuleFor(s => s.Address)
                .NotEmpty().WithMessage("Address is required.")
                .Length(1, 60).WithMessage("Address must be between 1 and 60 characters.");

            RuleFor(s => s.City)
                .NotEmpty().WithMessage("City is required.")
                .Length(1, 15).WithMessage("City must be between 1 and 15 characters.");

            RuleFor(s => s.Region)
                .Length(0, 15).WithMessage("Region must be between 0 and 15 characters.");

            RuleFor(s => s.PostalCode)
                .Length(0, 10).WithMessage("PostalCode must be between 0 and 10 characters.");

            RuleFor(s => s.Country)
                .NotEmpty().WithMessage("Country is required.")
                .Length(1, 15).WithMessage("Country must be between 1 and 15 characters.");

            RuleFor(s => s.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .Length(1, 24).WithMessage("Phone must be between 1 and 24 characters.");

            RuleFor(s => s.Fax)
                .Length(0, 24).WithMessage("Fax must be between 0 and 24 characters.");
        }
    }
}