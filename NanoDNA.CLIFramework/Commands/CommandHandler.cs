using NanoDNA.CLIFramework.Data;
using NanoDNA.CLIFramework.Flags;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NanoDNA.CLIFramework.Commands
{
    public abstract class CommandHandler
    {
        /// <summary>
        /// Global Flags preceding the Command Name, applies an application wide modifier to the command.
        /// </summary>
        public string[] GlobalFlags { get; protected set; }

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

        private void HandleArgs (string[] args)
        {
            List<string> globalFlags = new List<string>();
            int commandIndex = 0;
            string commandName = string.Empty;
            string[] commandArgs = new string[0];

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];

                if (GlobalFlagExists(arg))
                {
                    globalFlags.Add(arg);
                    continue;
                }

                if (CommandFactory.CommandExists(arg))
                {
                    commandIndex = i;
                    commandName = arg;
                    break;
                }
            }


            commandArgs = args.TakeLast(args.Length - commandIndex).ToArray();


           this.DataManager.GlobalFlags = globalFlags.ToArray();

            GlobalFlags = globalFlags.ToArray();
        }

        public void HandleCommand(string[] args)
        {
            HandleArgs(args);



        }

        /// <summary>
        /// Checks if the Global Flag Argument exists in the <see cref="CLIApplication"/>.
        /// </summary>
        /// <param name="flagArg">Flag Argument to verify</param>
        /// <returns>True if it is a valid Global Flag, False otherwise</returns>
        /// <exception cref="System.Exception">Thrown if an Invalid Flag was provided</exception>
        public bool GlobalFlagExists(string flagArg)
        {
            bool isGlobalFlag = flagArg.StartsWith(DataManager.GlobalFlagPrefix);
            bool isGlobalShorthandFlag = flagArg.StartsWith(DataManager.GlobalShorthandFlagPrefix);

            if (!isGlobalFlag && !isGlobalShorthandFlag)
                return false;

            string flag = isGlobalFlag ? flagArg.Replace(DataManager.GlobalFlagPrefix, "").Trim() : flagArg.Replace(DataManager.GlobalShorthandFlagPrefix, "").Trim();

            if (!FlagFactory.FlagExists(flag))
                throw new Exception($"Flag {flag} does not exist.");

            return true;
        }
    }
}
