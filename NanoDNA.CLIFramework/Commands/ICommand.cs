
namespace NanoDNA.CLIFramework.Commands
{
    /// <summary>
    /// Interface for Commands that are Executed in the CLI Application.
    /// </summary>
    internal interface ICommand
    {
        /// <summary>
        /// Name of the Command Executed.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Description of the Command Executed.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Function that handles executing the Command with Arguments
        /// </summary>
        /// <param name="args"> The Arguments for the Command </param>
        public abstract void Execute(string[] args);
    }
}
