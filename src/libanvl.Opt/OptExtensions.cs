namespace libanvl;

/// <summary>
/// Extension methods for working with <see cref="Opt{T}"/>
/// </summary>
public static class OptExtensions
{
    /// <summary>
    /// Returns the value if Some, or the <paramref name="default"/> if None.
    /// </summary>
    public static T SomeOrDefault<T>(this IOpt<T> opt, T @default) => opt.IsSome ? opt.Unwrap() : @default;

    /// <summary>
    /// Returns the string value if Some, or the empty string if None.
    /// </summary>
    public static string SomeOrEmpty(this IOpt<string> opt) => opt.SomeOrDefault(string.Empty);

    /// <summary>
    /// Returns the enumerable value if Some, or the empty enumerable if None.
    /// </summary>
    /// <typeparam name="T">The underlying type of the enumerable</typeparam>
    public static IEnumerable<T> SomeOrEmpty<T>(this IOpt<IEnumerable<T>> opt) => opt.SomeOrDefault(Enumerable.Empty<T>());

    /// <summary>
    /// For instance types, returns the value if Some, or null if None.
    /// </summary>
    /// <remarks>Should only be used to intentionally break out of the nullable context.</remarks>
    /// <typeparam name="T">The underlying instance type</typeparam>
    public static T? SomeOrNull<T>(this IOpt<T> opt) where T : class => opt.SomeOrDefault(null);

    /// <summary>
    /// Returns an iterator that iterates over the optional collection.
    /// </summary>
    /// <typeparam name="T">The underlying type of the enumerable</typeparam>
    public static IEnumerator<T> GetEnumerator<T>(this IOpt<IEnumerable<T>> opt) => opt.SomeOrEmpty().GetEnumerator();
}
