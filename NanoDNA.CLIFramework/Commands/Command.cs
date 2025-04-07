using System.Linq;
using NanoDNA.CLIFramework.Data;
using NanoDNA.CLIFramework.Flags;

namespace NanoDNA.CLIFramework.Commands
{
    /// <summary>
    /// Base Abstract Class for Defining a CLI Command.
    /// </summary>
    public abstract class Command : ICommand
    {
        /// <inheritdoc/>
        public IDataManager DataManager { get; }

        /// <inheritdoc/>
        public abstract string Name { get; }

        /// <inheritdoc/>
        public abstract string Description { get; }

        /// <summary>
        /// Initializes a new Instance of a <see cref="Command"/>.
        /// </summary>
        /// <param name="dataManager">Instance of the Data Manager used by the Command</param>
        protected Command(IDataManager dataManager)
        {
            DataManager = dataManager;
        }

        /// <inheritdoc/>
        public abstract void Execute(string[] args);

        /// <inheritdoc/>
        public bool HasFlag<T> () where T : Flag
        {
            return DataManager.GlobalFlags.Keys.Any(x => x == typeof(T));
        }
    }
}