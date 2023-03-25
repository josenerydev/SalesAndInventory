using FluentValidation;

namespace SalesAndInventory.Api.Models
{
    public class OrderDetailValidator : AbstractValidator<OrderDetail>
    {
        public OrderDetailValidator()
        {
            RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0)
                .WithMessage("UnitPrice must be greater than or equal to 0.");

            RuleFor(x => x.Qty).GreaterThan((short)0)
                .WithMessage("Qty must be greater than 0.");

            RuleFor(x => x.Discount).InclusiveBetween(0, 1)
                .WithMessage("Discount must be between 0 and 1.");
        }
    }
}