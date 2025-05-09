﻿using System;
using System.Linq;
using NanoDNA.CLIFramework.Data;
using NanoDNA.CLIFramework.Flags;
using System.Collections.Generic;

namespace NanoDNA.CLIFramework.Commands
{
    /// <summary>
    /// Defines the Base Command Handler, handles parsing of CLI Arguments and Routes to the appropriate Command Class.
    /// </summary>
    public class ArgumentHandler
    {
        /// <summary>
        /// Global Flags preceding the Command Name, applies an application wide modifier to the command.
        /// </summary>
        public Dictionary<Type, Flag> GlobalFlags { get; protected set; }

        /// <summary>
        /// Name of the Command to be executed.
        /// </summary>
        public string CommandName { get; protected set; }

        /// <summary>
        /// Arguments to be passed to the command.
        /// </summary>
        public string[] CommandArgs { get; protected set; }

        /// <summary>
        /// Index in the Args at which the Command Name was found.
        /// </summary>
        private int CommandIndex { get; set; }

        /// <summary>
        /// CLI Applications Settings, stores the Flag Prefixes and other settings.
        /// </summary>
        private ISetting Settings { get; }

        /// <summary>
        /// Initializes a new Instance of a <see cref="ArgumentHandler"/>.
        /// </summary>
        /// <param name="settings">CLI Applications Settings to use</param>
        public ArgumentHandler(ISetting settings)
        {
            Settings = settings;
            GlobalFlags = new Dictionary<Type, Flag>();
            CommandName = string.Empty;
            CommandArgs = new string[0];
        }

        /// <summary>
        /// Handles the Command Line Arguments and organizes them into Global Flags, Command Name and Command Arguments.
        /// </summary>
        /// <param name="args">CLI Arguments inputted</param>
        public void HandleArgs(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];

                if (CommandFactory.CommandExists(arg))
                {
                    CommandIndex = i;
                    CommandName = arg;
                    break;
                }
            }

            for (int i = 0; i < CommandIndex; i++)
            {
                string arg = args[i];

                if (GlobalFlagExists(arg))
                {
                    AddFlag(ref i, args);
                    continue;
                }
            }

            CommandArgs = args.TakeLast(args.Length - 1 - CommandIndex).ToArray();
        }

        /// <summary>
        /// Adds the Globa Flag to the Dictionary with it's Arguments.
        /// </summary>
        /// <param name="index">Inputted Arguments Index</param>
        /// <param name="args">CLI Arguments that were inputted</param>
        private void AddFlag(ref int index, string[] args)
        {
            string flagIdentifier = GetFlagIdentifier(args[index]);
            List<string> flagArgs = new List<string>();

            for (int j = index + 1; j < CommandIndex; j++)
            {
                string nextArg = args[j];

                if (IsFlag(nextArg))
                    break;

                flagArgs.Add(nextArg);
            }

            Flag flag = FlagFactory.GetFlag(flagIdentifier, flagArgs.ToArray());

            GlobalFlags.Add(flag.GetType(), flag);

            index += flagArgs.Count;
        }

        /// <summary>
        /// Checks if the Flag Argument is a Global Flag.
        /// </summary>
        /// <param name="flagArg">The Flag Argument to check</param>
        /// <returns>True if it is a Valid Flag Argument</returns>
        private bool IsGlobalFlag(string flagArg)
        {
            return flagArg.StartsWith(Settings.GlobalFlagPrefix);
        }

        /// <summary>
        /// Checks if the Flag Argument is a Shorthand Global Flag.
        /// </summary>
        /// <param name="flagArg">The Flag Argument to check</param>
        /// <returns>True if it is a Valid Shorthand Flag Argument</returns>
        private bool IsGlobalShorthandFlag(string flagArg)
        {
            return flagArg.StartsWith(Settings.GlobalShorthandFlagPrefix);
        }

        /// <summary>
        /// Checks if the Flag Argument is a Full or Shorthand Global Flag.
        /// </summary>
        /// <param name="flagArg">The Flag Argument to check</param>
        /// <returns>True if it is a Valid Flag Argument</returns>
        private bool IsFlag(string flagArg)
        {
            return IsGlobalFlag(flagArg) || IsGlobalShorthandFlag(flagArg);
        }

        /// <summary>
        /// Gets the Flag Identifier from the Flag Argument by removing the Flag Prefix.
        /// </summary>
        /// <param name="flagArg">Flag Argument to Clean</param>
        /// <returns>The Cleaned Fag identifier</returns>
        /// <exception cref="Exception">Thrown if the Prefix is invalid</exception>
        private string GetFlagIdentifier(string flagArg)
        {
            if (IsGlobalFlag(flagArg))
                return flagArg.Replace(Settings.GlobalFlagPrefix, "").Trim();

            if (IsGlobalShorthandFlag(flagArg))
                return flagArg.Replace(Settings.GlobalShorthandFlagPrefix, "").Trim();

            throw new Exception($"Invalid Flag Prefix in Argument: {flagArg}.");
        }

        /// <summary>
        /// Checks if the Global Flag Argument exists in the <see cref="CLIApplication{S, DM}"/>.
        /// </summary>
        /// <param name="flagArg">Flag Argument to verify</param>
        /// <returns>True if it is a valid Global Flag, False otherwise</returns>
        /// <exception cref="Exception">Thrown if an Invalid Flag was provided</exception>
        private bool GlobalFlagExists(string flagArg)
        {
            if (!IsFlag(flagArg))
                return false;

            string flag = IsGlobalFlag(flagArg) ? flagArg.Replace(Settings.GlobalFlagPrefix, "").Trim() : flagArg.Replace(Settings.GlobalShorthandFlagPrefix, "").Trim();

            if (!FlagFactory.FlagExists(flag))
                throw new Exception($"Flag \"{flag}\" does not exist.");

            return true;
        }
    }
}
