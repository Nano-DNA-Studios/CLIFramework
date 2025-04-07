using System;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using NanoDNA.CLIFramework.Data;
using System.Collections.Generic;

namespace NanoDNA.CLIFramework.Commands
{
    /// <summary>
    /// Factory for creating and managing Command Instances in the CLI Application.
    /// </summary>
    public static class CommandFactory
    {
        /// <summary>
        /// Dictionary of Commands that are available in the CLI.
        /// </summary>
        private static Dictionary<string, Type> _commands;

        /// <summary>
        /// Initializes a new Static Instance of a <see cref="CommandFactory"/>.
        /// </summary>
        static CommandFactory()
        {
            _commands = new Dictionary<string, Type>();
        }

        /// <summary>
        /// Loads all the Commands that are available in the assemblies associated with the CLI Application.
        /// </summary>
        public static void LoadCommands()
        {
            string currentAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                bool isCLIAssembly = assembly.GetName().Name == currentAssemblyName;
                bool isCLIFrameworkAssembly = assembly.GetReferencedAssemblies().Any(x => x.Name == currentAssemblyName);

                if (!isCLIFrameworkAssembly && !isCLIAssembly)
                    continue;

                foreach (Type type in assembly.GetTypes())
                {
                    if (!type.IsSubclassOf(typeof(Command)) || type.IsAbstract)
                        continue;

                    TryAddCommand(type);
                }
            }
        }

        /// <summary>
        /// Tries Adds a Command to the Dictionary of Available Commands.
        /// </summary>
        /// <param name="commandType">The Type of the Valid Command to try and add</param>
        private static void TryAddCommand(Type commandType)
        {
            try
            {
                Command flag = (Command)Activator.CreateInstance(commandType, new object[] { null }); ;

                if (flag == null)
                    return;

                _commands.Add(flag.Name, commandType);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating command instance: {ex.Message}");
                return;
            }
        }

        /// <summary>
        /// Checks if a Command exists in the CLI Application.
        /// </summary>
        /// <param name="commandIdentifier">Identifer for the Command (Name of the Command)</param>
        /// <returns>True if the Command has is loaded and exists, False otherwise</returns>
        public static bool CommandExists(string commandIdentifier)
        {
            return _commands.ContainsKey(commandIdentifier);
        }

        /// <summary>
        /// Gets a new Initialized Instance of a Command based on it's name.
        /// </summary>
        /// <param name="commandName">Name of the Command to Initialize</param>
        /// <param name="dataManager">Instance of the Data Manager to pass to the Command</param>
        /// <returns>New Instance of the Specified Command</returns>
        /// <exception cref="Exception">Thrown if the Command Name doesn't exist in the CLI Application</exception>
        public static Command GetCommand(string commandName, DataManager dataManager)
        {
            string commandNameLower = commandName.ToLower();

            if (_commands.TryGetValue(commandNameLower, out Type commandType))
            {
                if (commandType != null)
                    return Activator.CreateInstance(commandType, new object[] { dataManager }) as Command;
            }

            throw new Exception($"Command \"{commandName}\" does not exist.");
        }
    }
}