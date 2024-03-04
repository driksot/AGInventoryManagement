namespace AGInventoryManagement.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty();

        RuleFor(p => p.Sku).NotEmpty();
    }
}
