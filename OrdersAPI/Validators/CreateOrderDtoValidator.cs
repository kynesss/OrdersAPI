using FluentValidation;
using OrdersAPI.Models;

namespace OrdersAPI.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.Items)
                .NotEmpty()
                .Must(items => items.Count > 0).WithMessage("Order must contain at least 1 item")
                .ForEach(item => item.SetValidator(new CreateOrderItemDtoValidator()));
        }
    }
}