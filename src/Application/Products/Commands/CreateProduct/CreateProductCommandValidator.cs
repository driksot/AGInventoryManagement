namespace AGInventoryManagement.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty();

        RuleFor(p => p.Sku).NotEmpty();
    }
}
