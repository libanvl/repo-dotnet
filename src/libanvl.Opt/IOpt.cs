
namespace libanvl;

/// <summary>
/// Represents an optional value of type <typeparamref name="T"/>.
/// </summary>
/// <remarks>
/// This interface is primarily to support variance in extension methods.
/// </remarks>
/// <typeparam name="T"></typeparam>
public interface IOpt<out T>
{
    /// <summary>
    /// Whether this instance is a None value
    /// </summary>
    bool IsNone { get; }

    /// <summary>
    /// Whether this instance is a Some value
    /// </summary>
    bool IsSome { get; }

    /// <summary>
    /// Casts the <see cref="Opt{T}"/> to an <see cref="Opt{U}"/>.
    /// </summary>
    /// <remarks>
    /// The resulting <see cref="Opt{U}"/> will be <see cref="Opt{U}.None"/>
    /// if this instance is None, or the cast fails.
    /// </remarks>
    /// <typeparam name="U">The type to cast to.</typeparam>
    Opt<U> Cast<U>() where U : class;

    /// <summary>
    /// Projects the <see cref="Opt{T}"/> into a new form of <see cref="Opt{U}"/>.
    /// </summary>
    /// <typeparam name="U">The underlying type of the projection</typeparam>
    /// <param name="selector">The selector function</param>
    Opt<U> Select<U>(Func<T, U> selector);

    /// <summary>
    /// Returns the value if Some, or throws if None.
    /// </summary>
    /// <exception cref="InvalidOperationException" />
    T Unwrap();
}