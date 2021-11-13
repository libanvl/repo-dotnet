namespace libanvl;

/// <summary>
/// The abstract non-generic base of <see cref="Opt{T}"/>.
/// </summary>
/// <param name="ValueType">The underlying type</param>
public abstract record OptBase(Type ValueType);

/// <summary>
/// Represents a value that may or may not be present.
/// </summary>
/// <remarks>Most useful when in a #nullable enable context</remarks>
/// <typeparam name="T"></typeparam>
public abstract record Opt<T>() : OptBase(typeof(T)), IOpt<T>
{
    /// <inheritdoc />
    public static implicit operator Opt<T>(T value) => value.WrapOpt();

    /// <inheritdoc />
    public sealed record Some(T Value) : Opt<T>
    {
        /// <inheritdoc />
        public override bool IsSome => true;

        /// <inheritdoc />
        public override T Unwrap() => Value;

        /// <inheritdoc />
        public override Opt<U> Select<U>(Func<T, U> selector) => selector(Value).WrapOpt();

        /// <inheritdoc />
        public override Opt<U> Cast<U>() => (Value as U).WrapOpt();
    }

    private sealed record NoneImpl : Opt<T>
    {
        public override bool IsSome => false;

        public override T Unwrap() => throw new InvalidOperationException(typeof(T).AssemblyQualifiedName);

        public override Opt<U> Select<U>(Func<T, U> _) => Opt<U>.None;

        public override Opt<U> Cast<U>() => Opt<U>.None;
    }

    /// <summary>
    /// The single instance of None for <typeparamref name="T"/>.
    /// </summary>
    public static Opt<T> None { get; } = new NoneImpl();

    /// <inheritdoc />
    public bool IsNone => !IsSome;

    /// <inheritdoc />
    public abstract bool IsSome { get; }

    /// <inheritdoc />
    public abstract Opt<U> Select<U>(Func<T, U> selector);

    /// <inheritdoc />
    public abstract Opt<U> Cast<U>() where U : class;

    /// <inheritdoc />
    public abstract T Unwrap();
}

/// <summary>
/// Utility functions for making <see cref="Opt{T}"/> instances.
/// </summary>
public static class Opt
{
    /// <summary>
    /// Create an <see cref="Opt{T}.Some"/> for the given value.
    /// </summary>
    /// <typeparam name="T">The type of the value</typeparam>
    /// <param name="value">The value to wrap</param>
    public static Opt<T>.Some Some<T>(T value) => new(value);

    /// <summary>
    /// Get <see cref="Opt{T}.None"/> for a the given type <typeparamref name="T"/>.
    /// </summary>
    /// <remarks>
    /// In general it is preferable to use the <see cref="Opt{T}.None"/> property directly.
    /// </remarks>
    /// <typeparam name="T">The type of the Opt</typeparam>
    public static Opt<T> None<T>() => Opt<T>.None;
}