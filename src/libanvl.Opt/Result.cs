namespace libanvl;

/// <summary>
/// Represents a result that can either be a success (Ok) or an error (Err).
/// </summary>
/// <typeparam name="TResult">The type of the success value.</typeparam>
/// <typeparam name="TError">The type of the error value.</typeparam>
public readonly struct Result<TResult, TError>
    where TResult : notnull
    where TError : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TResult, TError}"/> struct with a success value.
    /// </summary>
    /// <param name="value">The success value.</param>
    public Result(TResult value)
    {
        Value = Opt.Some(value);
        Error = Opt<TError>.None;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TResult, TError}"/> struct with an error value.
    /// </summary>
    /// <param name="error">The error value.</param>
    public Result(TError error)
    {
        Value = Opt<TResult>.None;
        Error = Opt.Some(error);
    }

    /// <summary>
    /// Deconstructs the result into its value and error components.
    /// </summary>
    /// <param name="value">The success value, if present.</param>
    /// <param name="error">The error value, if present.</param>
    public void Deconstruct(out Opt<TResult> value, out Opt<TError> error)
    {
        value = Value;
        error = Error;
    }

    /// <summary>
    /// Gets the success value, if present.
    /// </summary>
    public Opt<TResult> Value { get; }

    /// <summary>
    /// Gets the error value, if present.
    /// </summary>
    public Opt<TError> Error { get; }

    /// <summary>
    /// Gets a value indicating whether the result is a success.
    /// </summary>
    public bool IsOk => Value.IsSome;

    /// <summary>
    /// Gets a value indicating whether the result is an error.
    /// </summary>
    public bool IsErr => Error.IsSome;

    /// <summary>
    /// Unwraps the success value, if present.
    /// </summary>
    /// <returns>The success value.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the result is an error.</exception>
    public TResult Unwrap() => Value.Unwrap();

    /// <summary>
    /// Matches the result, invoking the appropriate action based on whether it is a success or an error.
    /// </summary>
    /// <param name="ok">The action to invoke if the result is a success.</param>
    /// <param name="err">The action to invoke if the result is an error.</param>
    public void Match(Action<TResult> ok, Action<TError> err)
    {
        if (IsOk)
            ok(Value.Unwrap());
        else
            err(Error.Unwrap());
    }

    /// <summary>
    /// Matches the result, invoking the appropriate function based on whether it is a success or an error.
    /// </summary>
    /// <typeparam name="T">The type of the result of the function.</typeparam>
    /// <param name="ok">The function to invoke if the result is a success.</param>
    /// <param name="err">The function to invoke if the result is an error.</param>
    /// <returns>The result of the invoked function.</returns>
    public T Match<T>(Func<TResult, T> ok, Func<TError, T> err)
    {
        if (IsOk)
            return ok(Value.Unwrap());
        else
            return err(Error.Unwrap());
    }

    /// <summary>
    /// Returns the success value if present, otherwise returns the specified default value.
    /// </summary>
    /// <param name="defaultValue">The default value to return if the success value is not present.</param>
    /// <returns>The success value or the default value.</returns>
    public TResult OkOr(TResult defaultValue) => Value.SomeOr(defaultValue);

    /// <summary>
    /// Returns the success value if present, otherwise returns the result of the specified function.
    /// </summary>
    /// <param name="fn">The function to invoke if the success value is not present.</param>
    /// <returns>The success value or the result of the function.</returns>
    public TResult OkOr<T>(Func<TResult> fn) => Value.SomeOr(fn);

    /// <summary>
    /// Returns the success value if present, otherwise returns the default value of <typeparamref name="TResult"/>.
    /// </summary>
    /// <returns>The success value or the default value.</returns>
    public TResult? OkOrDefault() => Value.SomeOrDefault();

    /// <summary>
    /// Implicitly converts a success value to a <see cref="Result{TResult, TError}"/>.
    /// </summary>
    /// <param name="value">The success value.</param>
    public static implicit operator Result<TResult, TError>(TResult value) => new(value);

    /// <summary>
    /// Implicitly converts an error value to a <see cref="Result{TResult, TError}"/>.
    /// </summary>
    /// <param name="error">The error value.</param>
    public static implicit operator Result<TResult, TError>(TError error) => new(error);
}
