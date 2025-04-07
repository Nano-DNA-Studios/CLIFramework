
namespace NanoDNA.CLIFramework.Flags
{
    /// <summary>
    /// Defines a Base CLI Command Flag.
    /// </summary>
    public abstract class Flag : IFlag
    {
        /// <inheritdoc/>
        public abstract string Name { get; }

        /// <inheritdoc/>
        public abstract string ShorthandName { get; }

        /// <inheritdoc/>
        public abstract string Description { get; }

        /// <inheritdoc/>
        public string[] Arguments { get; protected set; }

        /// <summary>
        /// Initializes a new Instance of a <see cref="Flag"/>.
        /// </summary>
        /// <param name="arguments">Arguments associated with the Flag</param>
        public Flag(string[] arguments)
        {
            Arguments = arguments;
        }
    }
}
