﻿using System;
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
        /// CLI Applications Settings
        /// </summary>
        public Setting Settings { get; }

        /// <summary>
        /// The Current Working Directory the <see cref="CLIApplication{S, DM}"/> is ran from
        /// </summary>
        public string CWD { get; }

        /// <summary>
        /// The Cache Path for CWD related data. For storing temporary data or outputs related to a CLI Execution
        /// </summary>
        public string CWDCachePath { get; }

        /// <summary>
        /// Dictionary of Command Flags, Stores all available Flags in the <see cref="CLIApplication{S, DM}"/>
        /// </summary>
        public Dictionary<Type, Flag> GlobalFlags { get; }

        /// <summary>
        /// Checks if a Global Flag has been specified in the CLI Arguments.
        /// </summary>
        /// <typeparam name="T">Flag Class Instance Type</typeparam>
        /// <returns>True if the Global Flag had been indicated, False otherwise</returns>
        public bool HasFlag<T>() where T : Flag;
    }
}