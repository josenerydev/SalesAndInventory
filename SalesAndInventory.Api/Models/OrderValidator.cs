using FluentValidation;

namespace SalesAndInventory.Api.Models
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.OrderDate)
                .NotEmpty().WithMessage("Order date is required.");

            RuleFor(o => o.RequiredDate)
                .NotEmpty().WithMessage("Required date is required.")
                .GreaterThan(o => o.OrderDate).WithMessage("Required date must be after order date.");

            RuleFor(o => o.ShipName)
                .NotEmpty().WithMessage("Shipping name is required.")
                .MaximumLength(40).WithMessage("Shipping name must not exceed 40 characters.");

            RuleFor(o => o.ShipAddress)
                .NotEmpty().WithMessage("Shipping address is required.")
                .MaximumLength(60).WithMessage("Shipping address must not exceed 60 characters.");

            RuleFor(o => o.ShipCity)
                .NotEmpty().WithMessage("Shipping city is required.")
                .MaximumLength(15).WithMessage("Shipping city must not exceed 15 characters.");

            RuleFor(o => o.ShipRegion)
                .MaximumLength(15).WithMessage("Shipping region must not exceed 15 characters.");

            RuleFor(o => o.ShipPostalCode)
                .MaximumLength(10).WithMessage("Shipping postal code must not exceed 10 characters.");

            RuleFor(o => o.ShipCountry)
                .NotEmpty().WithMessage("Shipping country is required.")
                .MaximumLength(15).WithMessage("Shipping country must not exceed 15 characters.");
        }
    }
}