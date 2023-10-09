using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    /// <summary>
    /// Updated order command validator class used to validate the command
    /// </summary>
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                    .NotEmpty().WithMessage("{UserName} is required")
                    .NotNull().WithMessage("{UserName} cannot be null")
                    .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");

            RuleFor(p => p.EmailAddress)
                    .NotEmpty().WithMessage("{EmailAdress} is required")
                    .NotNull().WithMessage("{EmailAdress} cannot be null");

            RuleFor(p => p.TotalPrice)
                    .NotEmpty().WithMessage("{TotalPrice} is required")
                    .GreaterThan(0).WithMessage("{TotalPrice} should be greater than 0.");
        }
    }
}
