using System;
using NanoDNA.CLIFramework.Data;
using NanoDNA.CLIFramework.Flags;
using NanoDNA.CLIFramework.Commands;

namespace NanoDNA.CLIFramework
{
    /// <summary>
    /// Base Class for a CLI Application
    /// </summary>
    public abstract class CLIApplication<S, DM> where S : Setting, new() where DM : DataManager
    {
        /// <summary>
        /// Name of the <see cref="CLIApplication{S, DM}"/>
        /// </summary>
        public string Name { get => Settings.ApplicationName; }

        /// <summary>
        /// The <see cref="CLIApplication{S, DM}"/>'s Settings, used to manage the settings of the application.
        /// </summary>
        public S Settings { get; private set; }

        /// <summary>
        /// The <see cref="CLIApplication{S, DM}"/>'s DataManager, manages the data and state of the application.
        /// </summary>
        public DM DataManager { get; private set; }

        /// <summary>
        /// The <see cref="CLIApplication{S, DM}"/>'s CommandHandler, handles the commands and arguments passed to the application's Commands.
        /// </summary>
        public ArgumentHandler ArgumentHandler { get; private set; }

        /// <summary>
        /// Initializes a new Instance of a <see cref="CLIApplication{S, DM}"/>.
        /// </summary>
        public CLIApplication()
        {
            Settings = new S();
            ArgumentHandler = new ArgumentHandler(Settings);

            FlagFactory.LoadFlags();
            CommandFactory.LoadCommands();
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
            DataManager = (DM)Activator.CreateInstance(typeof(DM), new object[] { Settings, ArgumentHandler.GlobalFlags });

            Command command = CommandFactory.GetCommand(ArgumentHandler.CommandName, DataManager);

            command.Execute(ArgumentHandler.CommandArgs);
        }
    }
}