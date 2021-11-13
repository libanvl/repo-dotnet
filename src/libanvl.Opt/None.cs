namespace libanvl
{
    /// <summary>
    /// Utility properties for getting <see cref="Opt{T}.None"/> values.
    /// </summary>
    public static class None
    {
        /// <summary>
        /// Gets the value for a missing <see cref="string"/>.
        /// </summary>
        public static Opt<string> String => Opt<string>.None;

        /// <summary>
        /// Gets the value for a missing <see cref="int"/>.
        /// </summary>
        public static Opt<int> Int => Opt<int>.None;

        /// <summary>
        /// Gets the value for a missing <see cref="bool"/>.
        /// </summary>
        public static Opt<bool> Bool => Opt<bool>.None;

        /// <summary>
        /// Gets the value for a missing <see cref="Double"/>.
        /// </summary>
        public static Opt<double> Double => Opt<double>.None;
    }
}