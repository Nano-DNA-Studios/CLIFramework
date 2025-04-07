using NanoDNA.CLIFramework.Data;
using NanoDNA.CLIFramework.Flags;

namespace NanoDNA.CLIFramework.Commands
{
    /// <summary>
    /// Interface for Commands that are Executed in the CLI Application.
    /// </summary>
    internal interface ICommand
    {
        /// <summary>
        /// DataManager Instance storing the CLI Applications Data. 
        /// </summary>
        public abstract IDataManager DataManager { get; }

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

        /// <summary>
        /// Checks if the Type of Global Flag has been specified in the CLI Arguments.
        /// </summary>
        /// <typeparam name="T">Type of Flag to check for</typeparam>
        /// <returns>True if the Flag has been specied in the Global Flags, False otherwise</returns>
        public abstract bool HasFlag<T>() where T : Flag;
    }
}
