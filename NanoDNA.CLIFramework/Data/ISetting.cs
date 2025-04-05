using NanoDNA.CLIFramework.Flags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoDNA.CLIFramework.Data
{
    internal interface ISetting
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
        /// The Cache Path of the <see cref="CLIApplication"/>. For storing temporary data
        /// </summary>
        public string CachePath { get; }

        /// <summary>
        /// Prefix to use for Specifying Global Flags while using the <see cref="CLIApplication"/>
        /// </summary>
        public string GlobalFlagPrefix { get; }

        /// <summary>
        /// Prefix to use for Specifying Global Shorthand Flags while using the <see cref="CLIApplication"/>
        /// </summary>
        public string GlobalShorthandFlagPrefix { get; }
    }
}
