using Microsoft.EntityFrameworkCore;
using RackApi.Exceptions;

namespace RackApi.Helpers;

public static class ProductServiceHelper
{
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
}
