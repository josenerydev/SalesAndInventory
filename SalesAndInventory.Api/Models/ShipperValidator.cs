using FluentValidation;

namespace SalesAndInventory.Api.Models
{
    public class ShipperValidator : AbstractValidator<Shipper>
    {
        public ShipperValidator()
        {
            RuleFor(s => s.CompanyName)
                .NotEmpty().WithMessage("Company name is required.")
                .MaximumLength(40).WithMessage("Company name cannot exceed 40 characters.");

            RuleFor(s => s.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .MaximumLength(24).WithMessage("Phone cannot exceed 24 characters.");
        }
    }
}