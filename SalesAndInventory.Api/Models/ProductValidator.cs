using FluentValidation;

namespace SalesAndInventory.Api.Models
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(40).WithMessage("Product name cannot exceed 40 characters.");

            RuleFor(p => p.SupplierId)
                .NotEmpty().WithMessage("Supplier ID is required.")
                .GreaterThan(0).WithMessage("Supplier ID must be greater than zero.");

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("Category ID is required.")
                .GreaterThan(0).WithMessage("Category ID must be greater than zero.");

            RuleFor(p => p.UnitPrice)
                .NotEmpty().WithMessage("Unit price is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Unit price cannot be negative.");

            RuleFor(p => p.Discontinued)
                .NotNull().WithMessage("Discontinued flag is required.");
        }
    }
}