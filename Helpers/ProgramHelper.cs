using RackApi.Dto;
using RackApi.Exceptions;
using RackApi.Models;

namespace RackApi.Helpers;

public static class ProgramHelper
{
    public static Product MapDtoToProduct(ProductDto productDto)
    {
        return new Product
        {
            Name = productDto.Name,
            Url = productDto.Url,
            Vendor = productDto.Vendor,
            Price = productDto.Price,
            BeforeSalePrice = productDto.BeforeSalePrice,
        };
    }

    public static async Task<IResult> WrapAsyncServiceActionAndResult(Func<Task<IResult>> action)
    {
        try
        {
            return await action();
        }
        catch (CommonApiException ex)
        {
            string errorName = ex.GetType().Name;
            string errorMessage = ex.Message;
            return Results.Json(
                (ex is CommonDatabaseException)
                    ? new
                    {
                        Error = errorName,
                        InnerExceptionType = ex.InnerException?.GetType().Name,
                        InnerExceptionMessage = ex.InnerException?.Message,
                        errorMessage,
                    }
                    : new { Error = errorName, errorMessage },
                statusCode: ex.HttpStatusCode
            );
        }
    }
}
