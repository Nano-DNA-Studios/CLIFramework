
namespace NanoDNA.CLIFramework.Flags
{
    /// <summary>
    /// Defines a Base CLI Command Flag.
    /// </summary>
    public abstract class Flag : IFlag
    {
        /// <inheritdoc/>
        public abstract string Name { get; protected set; }

        /// <inheritdoc/>
        public abstract string ShorthandName { get; protected set; }

        /// <inheritdoc/>
        public abstract string Description { get; protected set; }
    }
}
