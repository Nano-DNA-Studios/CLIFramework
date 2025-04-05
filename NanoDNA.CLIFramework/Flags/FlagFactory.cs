using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace NanoDNA.CLIFramework.Flags
{
    /// <summary>
    /// Factory Class for Creating and Managing Flag Instances in the CLI Application.
    /// </summary>
    public static class FlagFactory
    {
        /// <summary>
        /// Dictionary of Flags that are available in the CLI.
        /// </summary>
        private static Dictionary<string, Type> _flags;

        /// <summary>
        /// Dictionary of Shorthand Flags that are available in the CLI.
        /// </summary>
        private static Dictionary<string, Type> _shorthandFlags;

        /// <summary>
        /// Initializes a new Instance of a <see cref="FlagFactory"/> on Program Start.
        /// </summary>
        static FlagFactory ()
        {
            _flags = new Dictionary<string, Type>();
            _shorthandFlags = new Dictionary<string, Type>();

            LoadFlags();
        }

        /// <summary>
        /// Loads all the Available Flags from the Assemblies associated with the CLI Application.
        /// </summary>
        public static void LoadFlags()
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
                    if (!type.IsSubclassOf(typeof(Flag)) || type.IsAbstract)
                        continue;

                    AddFlag(type);
                }
            }
        }

        /// <summary>
        /// Tries to Add a Flag to the Dictionary of Available Flags.
        /// </summary>
        /// <param name="flagType">Type of the Flag to add</param>
        private static void AddFlag (Type flagType)
        {
            try
            {
                Flag flag = (Flag)Activator.CreateInstance(flagType);

                if (flag == null)
                    return;

                _flags.Add(flag.Name, flagType);
                _shorthandFlags.Add(flag.ShorthandName, flagType);

            } catch (Exception ex)
            {
                Debug.WriteLine($"Error creating flag instance: {ex.Message}");
                return;
            }
        }

        /// <summary>
        /// Checks if the Flag identifier Maps to an existing Flag in the CLI Application.
        /// </summary>
        /// <param name="flagIdentifier">Flag Identifier to check</param>
        /// <returns>True if the identifier maps to a Flag, False otherwise</returns>
        public static bool FlagExists (string flagIdentifier)
        {
            return _flags.ContainsKey(flagIdentifier) || _shorthandFlags.ContainsKey(flagIdentifier);
        }

        /// <summary>
        /// Creates a new Instance of a Flag based on the Flag Identifier and Arguments provided.
        /// </summary>
        /// <param name="flagIdentifier"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Flag GetFlag(string flagIdentifier, string[] args)
        {
            string flagNameLower = flagIdentifier.ToLower();

            if (_flags.TryGetValue(flagNameLower, out Type flagType))
            {
                if (flagType != null)
                    return Activator.CreateInstance(flagType, [args]) as Flag;
            }

            if (_flags.TryGetValue(flagNameLower, out Type flagShorthandType))
            {
                if (flagShorthandType != null)
                    return Activator.CreateInstance(flagShorthandType, [args]) as Flag;
            }

            throw new Exception($"Flag \"{flagIdentifier}\" does not exist.");
        }
    }
}
