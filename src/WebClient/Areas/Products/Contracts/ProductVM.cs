using System.ComponentModel.DataAnnotations;

namespace AGInventoryManagement.WebClient.Areas.Products.Contracts;

public class ProductVM
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is a required field")]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required(ErrorMessage = "Price is a required field")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Sku is a required field")]
    public string Sku { get; set; } = string.Empty;

    public bool IsArchived { get; set; }
}
