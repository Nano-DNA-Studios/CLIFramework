using NanoDNA.CLIFramework.Flags;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NanoDNA.CLIFramework.Data
{
    /// <summary>
    /// Defines the Base DataManager and Stores info related to the <see cref="CLIApplication"/>.
    /// </summary>
    public abstract class DataManager : IDataManager
    {
        /// <inheritdoc/>
        public string ApplicationName { get; protected set; }

        /// <inheritdoc/>
        public string ApplicationPath { get; protected set; }

        /// <inheritdoc/>
        public string CWD { get; protected set; }

        /// <inheritdoc/>
        public string CachePath { get; protected set; }

        /// <inheritdoc/>
        public string CWDCachePath { get; protected set; }

        /// <inheritdoc/>
        public string GlobalFlagPrefix { get; protected set; } = "--";

        /// <inheritdoc/>
        public string GlobalShorthandFlagPrefix { get; protected set; } = "-";

        /// <inheritdoc/>
        public Dictionary<string, Flag> ApplicationFlags { get; protected set; }

        public string[] GlobalFlags { get; protected set; }

        /// <summary>
        /// Initializes a new Instance of a <see cref="DataManager"/>.
        /// </summary>
        /// <param name="applicationName">Name of the <see cref="CLIApplication"/></param>
        internal DataManager(string applicationName)
        {
            ApplicationName = applicationName;

            ApplicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            CWD = Directory.GetCurrentDirectory();
            CachePath = Path.Combine(ApplicationPath, "Cache");
            CWDCachePath = Path.Combine(CachePath, CWD);

            if (!Directory.Exists(CachePath))
                Directory.CreateDirectory(CachePath);
        }

        public bool HasFlag<T>() where T : Flag
        {
            return GlobalFlags.Any(x => x.GetType() == typeof(T));
        }

        public void SetGlobalFlag(string flag)
        {
            if (GlobalFlags == null)
                GlobalFlags = new string[] { flag };
            else
                GlobalFlags = GlobalFlags.Append(flag).ToArray();
        }





    }
}
