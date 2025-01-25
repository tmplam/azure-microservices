namespace ProductsService.BusinessLogic.Dtos;

public record ProductAddRequest(
    string ProductName,
    CategoryOptions Category,
    double UnitPrice,
    int QuantityInStock)
{
    public ProductAddRequest() : this(string.Empty, CategoryOptions.Electronics, 0, 0) { }
}