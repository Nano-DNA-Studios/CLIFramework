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
        public ArgumentHandler ArgumentHandler { get; private set; }

        /// <summary>
        /// Initializes a new Instance of a <see cref="CLIApplication"/>.
        /// </summary>
        /// <param name="dataManager"><see cref="IDataManager"/> to use to manager Application Data</param>
        /// <param name="commandHandler">Command Handler type to take care of </param>
        public CLIApplication (IDataManager dataManager, ArgumentHandler commandHandler)
        {
            DataManager = dataManager;
            ArgumentHandler = commandHandler;
        }

        /// <summary>
        /// Runs the CLI Application by passing Arguments to the Command Handler to route it to the Command.
        /// </summary>
        /// <param name="args">CLI Arguments Inputted</param>
        /// <exception cref="Exception">thrown if the Command Handler is Undefined</exception>
        public void Run(string[] args)
        {
            if (ArgumentHandler == null)
                throw new Exception("Command Handler is null");

            ArgumentHandler.HandleArgs(args);

            Command command = CommandFactory.GetCommand(ArgumentHandler.CommandName);

            command.Execute(ArgumentHandler.CommandArgs);
        }
    }
}