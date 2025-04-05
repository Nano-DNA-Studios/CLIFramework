using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace NanoDNA.CLIFramework.Commands
{
    internal class CommandFactory
    {
        private static Dictionary<string, Type> _commands;


        static CommandFactory()
        {
            _commands = new Dictionary<string, Type>();

            LoadCommands();
        }

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

                    AddCommand(type);
                }
            }
        }

        private static void AddCommand (Type commandType)
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









    }
}
