using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace NanoDNA.CLIFramework.Flags
{
    public static class FlagFactory
    {
        private static Dictionary<string, Type> _flags;

        private static Dictionary<string, Type> _shorthandFlags;

        static FlagFactory ()
        {
            _flags = new Dictionary<string, Type>();
            _shorthandFlags = new Dictionary<string, Type>();

            LoadFlags();
        }

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

        private static void AddFlag (Type flagType)
        {
            try
            {
                Flag? flag = (Flag?)Activator.CreateInstance(flagType);

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

        public static bool FlagExists (string flagIdentifier)
        {
            return _flags.ContainsKey(flagIdentifier) || _shorthandFlags.ContainsKey(flagIdentifier);
        }

    }
}
