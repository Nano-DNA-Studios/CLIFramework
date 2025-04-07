
namespace NanoDNA.CLIFramework.Data
{
    /// <summary>
    /// Interface defining the contract for a setting.
    /// </summary>
    public interface ISetting
    {
        /// <summary>
        /// Name of the <see cref="CLIApplication{S, DM}"/>
        /// </summary>
        public string ApplicationName { get; }

        /// <summary>
        /// Path to the <see cref="CLIApplication{S, DM}"/> DLLs
        /// </summary>
        public string ApplicationPath { get; }

        /// <summary>
        /// The Cache Path of the <see cref="CLIApplication{S, DM}"/>. For storing temporary data
        /// </summary>
        public string CachePath { get; }

        /// <summary>
        /// Prefix to use for Specifying Global Flags while using the <see cref="CLIApplication{S, DM}"/>
        /// </summary>
        public string GlobalFlagPrefix { get; }

        /// <summary>
        /// Prefix to use for Specifying Global Shorthand Flags while using the <see cref="CLIApplication{S, DM}"/>
        /// </summary>
        public string GlobalShorthandFlagPrefix { get; }
    }
}
