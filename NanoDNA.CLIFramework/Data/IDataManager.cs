
using NanoDNA.CLIFramework.Flags;
using System.Collections.Generic;

namespace NanoDNA.CLIFramework.Data
{
    /// <summary>
    /// Interface defining the contract for a data manager.
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// Name of the <see cref="CLIApplication"/>
        /// </summary>
        public string ApplicationName { get; }

        /// <summary>
        /// Path to the <see cref="CLIApplication"/> DLLs
        /// </summary>
        public string ApplicationPath { get; }

        /// <summary>
        /// The Current Working Directory the <see cref="CLIApplication"/> is ran from
        /// </summary>
        public string CWD { get; }

        /// <summary>
        /// The Cache Path of the <see cref="CLIApplication"/>. For storing temporary data
        /// </summary>
        public string CachePath { get; }

        /// <summary>
        /// The Cache Path for CWD related data. For storing temporary data or outputs related to a CLI Execution
        /// </summary>
        public string CWDCachePath { get; }

        /// <summary>
        /// Prefix to use for Specifying Global Flags while using the <see cref="CLIApplication"/>
        /// </summary>
        public string GlobalFlagPrefix { get; }

        /// <summary>
        /// Prefix to use for Specifying Global Shorthand Flags while using the <see cref="CLIApplication"/>
        /// </summary>
        public string GlobalShorthandFlagPrefix { get; }

        /// <summary>
        /// Dictionary of Command Flags, Stores all available Flags in the <see cref="CLIApplication"/>
        /// </summary>
        public Dictionary<string, Flag> ApplicationFlags { get; }
    }
}
