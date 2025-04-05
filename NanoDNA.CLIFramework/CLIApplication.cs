using NanoDNA.CLIFramework.Data;
using NanoDNA.CLIFramework.Commands;
using System;

namespace NanoDNA.CLIFramework
{
    /// <summary>
    /// Base Class for a CLI Application
    /// </summary>
    public abstract class CLIApplication
    {
        /// <summary>
        /// Name of the <see cref="CLIApplication"/>
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The <see cref="CLIApplication"/>'s DataManager, manages the data and state of the application.
        /// </summary>
        public static IDataManager DataManager { get; private set; }

        /// <summary>
        /// The <see cref="CLIApplication"/>'s CommandHandler, handles the commands and arguments passed to the application's Commands.
        /// </summary>
        public CommandHandler CommandHandler { get; private set; }

        /// <summary>
        /// Initializes a new Instance of a <see cref="CLIApplication"/>.
        /// </summary>
        /// <param name="dataManager"><see cref="IDataManager"/> to use to manager Application Data</param>
        /// <param name="commandHandler">Command Handler type to take care of </param>
        public CLIApplication (IDataManager dataManager, CommandHandler commandHandler)
        {
            DataManager = dataManager;
            CommandHandler = commandHandler;
        }

        public void Run(string[] args)
        {
            if (CommandHandler == null)
                throw new Exception("Command Handler is null");

            CommandHandler.HandleCommand(args);
        }




    }
}
