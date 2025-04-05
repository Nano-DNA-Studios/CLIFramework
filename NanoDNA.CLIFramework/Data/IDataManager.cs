using NanoDNA.CLIFramework.Flags;
using System;
using System.Collections.Generic;

namespace NanoDNA.CLIFramework.Data
{
    /// <summary>
    /// Interface defining the contract for a data manager.
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// CLI Applications Settings
        /// </summary>
        public Setting Settings { get; }

        /// <summary>
        /// The Current Working Directory the <see cref="CLIApplication"/> is ran from
        /// </summary>
        public string CWD { get; }

        /// <summary>
        /// The Cache Path for CWD related data. For storing temporary data or outputs related to a CLI Execution
        /// </summary>
        public string CWDCachePath { get; }

        /// <summary>
        /// Dictionary of Command Flags, Stores all available Flags in the <see cref="CLIApplication"/>
        /// </summary>
        public Dictionary<Type, Flag> GlobalFlags { get; }
    }
}