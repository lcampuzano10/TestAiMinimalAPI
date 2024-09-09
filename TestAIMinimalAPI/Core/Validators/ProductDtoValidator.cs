using FluentValidation;
using TestAiMinimalAPI.Core.DTOs;

namespace TestAiMinimalAPI.Core.Validators;

public class ProductDtoValidator : AbstractValidator<ProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(_ => _.Name).NotEmpty();
        RuleFor(_ => _.Price).GreaterThan(0);
    }
}
