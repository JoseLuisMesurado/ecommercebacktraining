using ecommerce.application.Product.Commands;
using FluentValidation;

namespace ecommerce.api.EndpointValidators
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(command => command.BrandId)
                    .NotEmpty();
            RuleFor(command => command.CategoryId)
                    .NotEmpty();
            RuleFor(command => command.Description)
                    .NotEmpty();
            RuleFor(command => command.Name)
                    .NotEmpty();
            RuleFor(command => command.Price)
                    .NotEmpty();
            RuleFor(command => command.Stock)
                    .NotEmpty();
            RuleFor(command => command.Thumbnail)
                    .NotEmpty();
        }
    }
}
