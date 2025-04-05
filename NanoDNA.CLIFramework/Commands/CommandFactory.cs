using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace NanoDNA.CLIFramework.Commands
{
    /// <summary>
    /// Factory for creating and managing Command Instances in the CLI Application.
    /// </summary>
    internal class CommandFactory
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

            LoadCommands();
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
        private static void TryAddCommand (Type commandType)
        {
            try
            {
                Command? flag = (Command?)Activator.CreateInstance(commandType);

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

        public static bool CommandExists(string commandIdentifier)
        {
            return _commands.ContainsKey(commandIdentifier);
        }
    }
}