using System.IO;
using System.Reflection;

namespace NanoDNA.CLIFramework.Data
{
    /// <summary>
    /// Defines the Settings for a CLI Application.
    /// </summary>
    public abstract class Setting : ISetting
    {
        /// <summary>
        /// The Default Global Flag Prefix for the <see cref="CLIApplication{S, DM}"/>. Used by most CLI Applications.
        /// </summary>
        public const string DEFAULT_GLOBAL_FLAG_PREFIX = "--";

        /// <summary>
        /// The Default Global Shorthand Flag Prefix for the <see cref="CLIApplication{S, DM}"/>.. Used by most CLI Applications.
        /// </summary>
        public const string DEFAULT_GLOBAL_SHORTHAND_FLAG_PREFIX = "-";

        /// <inheritdoc/>
        public abstract string ApplicationName { get; }

        /// <inheritdoc/>
        public string ApplicationPath { get; }

        /// <inheritdoc/>
        public abstract string GlobalFlagPrefix { get; }

        /// <inheritdoc/>
        public abstract string GlobalShorthandFlagPrefix { get; }

        /// <inheritdoc/>
        public string CachePath { get; protected set; }

        /// <summary>
        /// Initializes a new Instance of a <see cref="Setting"/>.
        /// </summary>
        public Setting()
        {
            ApplicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            CachePath = Path.Combine(ApplicationPath, "Cache");

            if (!Directory.Exists(CachePath))
                Directory.CreateDirectory(CachePath);
        }
    }
}