using FluentValidation.Results;
using TaskManagement.Shared;

namespace TaskManagement.Application.Extensions;
public static class FluentValidationExtensions
{
    public static Result CreateErrorResult(this ValidationResult validationResult)
        => CreateErrorResult(validationResult, (error) => Result.Error(error));

    public static Result<T> CreateErrorResult<T>(this ValidationResult validationResult)
        => CreateErrorResult(validationResult, (error) => Result.Error<T>(error));

    private static T CreateErrorResult<T>(ValidationResult validationResult, Func<string, T> createResut)
        where T : Result
    {
        if (validationResult.IsValid)
        {
            throw new InvalidOperationException($"ValidationResult.IsValid = {validationResult.IsValid}");
        }

        string error = string.Join(Environment.NewLine, validationResult.Errors.Select(x => x.ErrorMessage));

        return createResut(error);
    }
}
