using FluentValidation;
using ProductsService.BusinessLogic.Dtos;
using ProductsService.BusinessLogic.ServiceContracts;

namespace ProductsService.API.Endpoints;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products", async (IProductsService productsService) =>
        {
            var products = await productsService.GetProductsAsync();
            return Results.Ok(products);
        });

        app.MapGet("/api/products/search/product-id/{productId:guid}", async (
            Guid productId,
            IProductsService productsService) =>
        {
            var product = await productsService.GetProductByConditionAsync(p => p.ProductID == productId);
            return Results.Ok(product);
        });


        app.MapGet("/api/products/search/{searchString}", async (
            string searchString,
            IProductsService productsService) =>
        {
            var products = await productsService.GetProductsByConditionAsync(p => 
                p.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                p.Category.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            return Results.Ok(products);
        });

        app.MapPost("/api/products", async (
            ProductAddRequest productAddRequest,
            IValidator<ProductAddRequest> productAddRequestValidator,
            IProductsService productsService) =>
        {
            var validationResult = await productAddRequestValidator.ValidateAsync(productAddRequest);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(t => t.PropertyName)
                    .ToDictionary(
                        t => t.Key,
                        t => t.Select(t => t.ErrorMessage).ToArray());

                return Results.ValidationProblem(errors);
            }

            var product = await productsService.AddProductAsync(productAddRequest);

            if (product is null)
                return Results.Problem("Error in adding product");

            return Results.Created($"/api/products/search/product-id/{product.ProductID}", product);
        });

        app.MapPut("/api/products", async (
            ProductUpdateRequest productUpdateRequest,
            IValidator<ProductUpdateRequest> productUpdateRequestValidator,
            IProductsService productsService) =>
        {
            var validationResult = await productUpdateRequestValidator.ValidateAsync(productUpdateRequest);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(t => t.PropertyName)
                    .ToDictionary(
                        t => t.Key,
                        t => t.Select(t => t.ErrorMessage).ToArray());

                return Results.ValidationProblem(errors);
            }

            var product = await productsService.UpdateProductAsync(productUpdateRequest);

            if (product is null)
                return Results.Problem("Error in updating product");

            return Results.Ok(product);
        });

        app.MapDelete("/api/products/{productId:guid}", async (
            Guid productId,
            IProductsService productsService) =>
        {
            var isDeleted = await productsService.DeleteProductAsync(productId);

            if (!isDeleted)
                return Results.Problem("Error in deleting product");

            return Results.Ok(true);
        });


        return app;
    }
}
