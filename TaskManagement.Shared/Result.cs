namespace TaskManagement.Shared;

/// <summary>
/// Wrapper object that holds information of some method result.
/// </summary>
/// <param name="Success">
/// True, if method has successfully finished and has possible result. 
/// False, if method  has not successfully finished and has error.
/// </param>
/// <param name="ErrorMessage">Error message if <paramref name="Success"/> is false.</param>
public record Result(bool Success, string? ErrorMessage = null)
{

    /// <summary>
    /// Creates OK result when method has successfully finished.
    /// </summary>
    /// <returns>Instance of <see cref="Result"/></returns>
    public static Result Ok() => new(true);

    /// <summary>
    /// Creates OK result when method has successfully finished and has result of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Type of result value.</typeparam>
    /// <returns>Instance of <see cref="Result{T}"/></returns>
    public static Result<T> Ok<T>(T value) => new(true, value);

    /// <summary>
    /// Creates Error result when method has not successfully finished and has error
    /// </summary>
    /// <param name="error">Error message.</param>
    /// <returns>Instance of <see cref="Result{T}"/></returns>
    public static Result Error(string error) => new(false, error);

    /// <summary>
    /// Creates Error result when method has not successfully finished and has error
    /// </summary>
    /// <typeparam name="T">Type of result value.</typeparam>
    /// <param name="error">Error message.</param>
    /// <returns>Instance of <see cref="Result{T}"/></returns>
    public static Result<T> Error<T>(string error) => new(false, default, error);
}

/// <summary>
/// Wrapper object that holds information of some method result and its value of <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">Type of result's value.</typeparam>
/// <param name="ErrorMessage"></param>
/// <param name="Success">
/// True, if method has successfully finished and has possible result. 
/// False, if method  has not successfully finished and has error.
/// </param>
/// <param name="Value">Some value as result of method execution.</param>
/// <param name="ErrorMessage">Error message if <paramref name="Success"/> is false.</param>
public record Result<T>(bool Success, T? Value = default, string? ErrorMessage = null) : Result(Success, ErrorMessage)
{
}
