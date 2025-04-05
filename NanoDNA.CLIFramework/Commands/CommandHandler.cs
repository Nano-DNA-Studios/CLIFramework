using NanoDNA.CLIFramework.Data;
using NanoDNA.CLIFramework.Flags;
using System.Collections.Generic;

namespace NanoDNA.CLIFramework.Commands
{
    public abstract class CommandHandler
    {
        /// <summary>
        /// Global Flags preceding the Command Name, applies an application wide modifier to the command.
        /// </summary>
        public Flag[] GlobalFlags { get; protected set; }

        /// <summary>
        /// Name of the Command to be executed.
        /// </summary>
        public string CommandName { get; protected set; }

        /// <summary>
        /// Arguments to be passed to the command.
        /// </summary>
        public string[] CommandArgs { get; protected set; }

        private IDataManager DataManager { get; set; }

        public CommandHandler()
        {
            GlobalFlags = new Flag[0];
            CommandName = string.Empty;
            CommandArgs = new string[0];

            DataManager = CLIApplication.DataManager;
        }

        public void HandleCommand(string[] args)
        {
            List<string> globalFlags = new List<string>();

            foreach (string arg in args)
            {
                if (GlobalFlagExists(arg))
                {
                    globalFlags.Add(arg);
                    continue;
                }



            }

            GlobalFlags = globalFlags.ToArray();



        }

        public bool GlobalFlagExists(string flagArg)
        {
            bool isGlobalFlag = flagArg.StartsWith(DataManager.GlobalFlagPrefix);
            bool isGlobalShorthandFlag = flagArg.StartsWith(DataManager.GlobalShorthandFlagPrefix);

            if (!isGlobalFlag && !isGlobalShorthandFlag)
                return false;

            string flag = isGlobalFlag ? flagArg.Replace(DataManager.GlobalFlagPrefix, "") : flagArg.Replace(DataManager.GlobalShorthandFlagPrefix, "");

            if (!FlagFactory.FlagExists(flag))
                throw new System.Exception($"Flag {flag} does not exist.");

            return true;
        }
    }
}
