namespace libanvl;

/// <summary>
/// Extensions methods for wrapping types in <see cref="Opt{T}"/>.
/// </summary>
public static class WrapExtensions
{
    /// <summary>
    /// Wrap <paramref name="value"/> in an appropriate <see cref="Opt{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <paramref name="value"/> to wrap</typeparam>
    /// <param name="value">The value to wrap</param>
    /// <returns><see cref="Opt{T}.None"/> if the value is <c>null</c>, <see cref="Opt{T}.Some"/> otherwise.</returns>
    public static Opt<T> WrapOpt<T>(this T? value) => value is null ? Opt.None<T>() : Opt.Some(value);

    /// <summary>
    /// Project <paramref name="value"/> to a new value and wrap in an appropirate <see cref="Opt{U}"/>.
    /// </summary>
    /// <typeparam name="T">The original type</typeparam>
    /// <typeparam name="U">The projected type</typeparam>
    /// <param name="value">The original value</param>
    /// <param name="projector">The projection function</param>
    public static Opt<U> WrapOpt<T, U>(this T? value, Func<T, U> projector) => value is null ? Opt<U>.None : projector(value).WrapOpt();

    /// <summary>
    /// Wrap a string in an appropriate <see cref="Opt{T}"/>.
    /// </summary>
    /// <param name="value">The string to wrap</param>
    /// <param name="whitespaceIsNone">Whether to treat whitespace strings the same as null and empty string</param>
    /// <returns><see cref="Opt{T}.None"/> if the string is null or empty (or whitespace if <paramref name="whitespaceIsNone"/> is <c>true</c>), <see cref="Opt{T}.Some"/> otherwise.</returns>
    public static Opt<string> WrapOpt(this string? value, bool whitespaceIsNone = false)
    {
        return whitespaceIsNone
            ? string.IsNullOrWhiteSpace(value) ? None.String : Opt.Some(value)
            : string.IsNullOrEmpty(value) ? None.String : Opt.Some(value);
    }

    /// <summary>
    /// Wrap an IEnumerable in an appropirate <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T">The collection type</typeparam>
    /// <param name="value">The enumerable to warp</param>
    /// <param name="emptyIsNone">Whether to treat empty enumerables the same as a null one</param>
    /// <returns><see cref="Opt{T}.None"/> if the value is null (or empty if <paramref name="emptyIsNone"/> is <c>true</c>, an <see cref="Opt{T}.Some"/> otherwise.</returns>
    public static Opt<IEnumerable<T>> WrapOpt<T>(this IEnumerable<T>? value, bool emptyIsNone = false)
    {
        if (value is not null)
        {
            return emptyIsNone
                ? value.Any() ? Opt.Some(value) : Opt.None<IEnumerable<T>>()
                : Opt.Some(value);
        }

        return Opt.None<IEnumerable<T>>();
    }
}
