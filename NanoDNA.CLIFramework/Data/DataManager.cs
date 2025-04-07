using System;
using System.IO;
using System.Linq;
using NanoDNA.CLIFramework.Flags;
using System.Collections.Generic;

namespace NanoDNA.CLIFramework.Data
{
    /// <summary>
    /// Defines the Base DataManager and Stores info related to the <see cref="CLIApplication{S, DM}"/>.
    /// </summary>
    public abstract class DataManager : IDataManager
    {
        /// <inheritdoc/>
        public Setting Settings { get; protected set; }

        /// <inheritdoc/>
        public string CWD { get; protected set; }

        /// <inheritdoc/>
        public string CWDCachePath { get; protected set; }

        /// <inheritdoc/>
        public Dictionary<Type, Flag> GlobalFlags { get; protected set; }

        /// <summary>
        /// Initializes a new Instance of a <see cref="DataManager"/>.
        /// </summary>
        /// <param name="settings">CLI Applications Settings to use</param>
        /// <param name="globalFlags">Global Flags inputted in the CLI Arguments</param>
        public DataManager(Setting settings, Dictionary<Type, Flag> globalFlags)
        {
            Settings = settings;
            GlobalFlags = globalFlags;

            CWD = Directory.GetCurrentDirectory();
            CWDCachePath = Path.Combine(CWD, $"{Settings.ApplicationName}Cache");
        }

        /// <inheritdoc/>
        public bool HasFlag<T>() where T : Flag
        {
            return GlobalFlags.Keys.Any(x => x == typeof(T));
        }
    }
}