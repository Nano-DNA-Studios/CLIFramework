using System;

namespace NanoDNA.CLIFramework.Commands
{
    /// <summary>
    /// Base Abstract Class for Defining a CLI Command.
    /// </summary>
    public abstract class Command : ICommand
    {
        /// <inheritdoc/>
        public abstract string Name { get; }

        /// <inheritdoc/>
        public abstract string Description { get; }

        /// <inheritdoc/>
        public abstract void Execute(string[] args);
    }
}
