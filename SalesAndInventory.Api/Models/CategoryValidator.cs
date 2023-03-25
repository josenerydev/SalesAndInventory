using FluentValidation;

namespace SalesAndInventory.Api.Models
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.CategoryName)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(15).WithMessage("Category name must have at most 15 characters.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(200).WithMessage("Description must have at most 200 characters.");
        }
    }
}