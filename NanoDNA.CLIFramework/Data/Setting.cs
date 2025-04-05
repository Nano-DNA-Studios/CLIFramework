using System.IO;
using System.Reflection;

namespace NanoDNA.CLIFramework.Data
{
    /// <summary>
    /// Defines the Settings for a CLI Application.
    /// </summary>
    public abstract class Setting : ISetting
    {
        /// <inheritdoc/>
        public abstract string ApplicationName { get; }

        /// <inheritdoc/>
        public abstract string ApplicationPath { get; protected set; }

        /// <inheritdoc/>
        public string GlobalFlagPrefix { get; } = "--";

        /// <inheritdoc/>
        public string GlobalShorthandFlagPrefix { get; } = "-";

        /// <inheritdoc/>
        public abstract string CachePath { get; protected set; }

        /// <summary>
        /// Initializes a new Instance of a <see cref="Setting"/>.
        /// </summary>
        public Setting ()
        {
            ApplicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            CachePath = Path.Combine(ApplicationPath, "Cache");

            if (!Directory.Exists(CachePath))
                Directory.CreateDirectory(CachePath);
        }
    }
}