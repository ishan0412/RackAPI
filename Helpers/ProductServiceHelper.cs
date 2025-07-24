using Microsoft.EntityFrameworkCore;
using RackApi.Exceptions;
using RackApi.Models;

namespace RackApi.Helpers;

public static class ProductServiceHelper
{
    private static readonly string[] NON_NULLABLE_FIELDS = ["Name", "Url", "Price", "Vendor"];

    public static async Task<T> WrapAsyncDbOperation<T>(Func<Task<T>> dbOperation)
    {
        try
        {
            return await dbOperation();
        }
        catch (Exception ex)
            when (ex is DbUpdateException
                || ex is DbUpdateConcurrencyException
                || ex is OperationCanceledException
                || ex is Npgsql.PostgresException
            )
        {
            throw new CommonDatabaseException(ex);
        }
    }

    public static void ThrowExceptionIfRequiredFieldIsNull(Product product)
    {
        foreach (string fieldName in NON_NULLABLE_FIELDS)
        {
            if (product.GetType().GetProperty(fieldName)?.GetValue(product) is null)
            {
                throw new NullFieldValueException(fieldName);
            }
        }
    }
}
