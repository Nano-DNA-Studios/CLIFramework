using System;
using System.Linq;
using NUnit.Framework;
using NanoDNA.CLIFramework.Flags;
using NanoDNA.CLIFramework.Commands;
using NanoDNA.CLIFramework.Tests.Application;

namespace NanoDNA.CLIFramework.Tests.Tests
{
    /// <summary>
    /// Tests the ArgumentHandler class.
    /// </summary>
    internal class ArgumentHandlerTests
    {
        /// <summary>
        /// Tests the initialization of the ArgumentHandler class.
        /// </summary>
        [Test]
        public void ArgumentHandlerInitializationTest()
        {
            UnitTestSettings settings = new UnitTestSettings();

            Assert.IsNotNull(settings, "Settings should be initialized");

            ArgumentHandler argumentHandler = new ArgumentHandler(settings);

            Assert.IsNotNull(argumentHandler, "ArgumentHandler should be initialized");
            Assert.IsNotNull(argumentHandler.CommandName, "Command Name should be initialized");
            Assert.IsNotNull(argumentHandler.GlobalFlags, "Global Flags should be initialized");
            Assert.IsNotNull(argumentHandler.CommandArgs, "Settings should be initialized");

            Assert.That(argumentHandler.CommandName, Is.EqualTo(string.Empty), "Command Name should be set to an empty string");
            Assert.That(argumentHandler.GlobalFlags.Count, Is.EqualTo(0), "Global Flags should be empty");
            Assert.That(argumentHandler.CommandArgs.Count, Is.EqualTo(0), "Command Args should be empty");
        }

        /// <summary>
        /// Tests the HandleArgs method of the ArgumentHandler class.
        /// </summary>
        [Test]
        public void HandleArgsTestCommandName()
        {
            CommandFactory.LoadCommands();
            FlagFactory.LoadFlags();

            UnitTestSettings settings = new UnitTestSettings();

            Assert.IsNotNull(settings, "Settings should be initialized");

            ArgumentHandler argumentHandler = new ArgumentHandler(settings);

            Assert.IsNotNull(argumentHandler, "ArgumentHandler should be initialized");
            Assert.IsNotNull(argumentHandler.CommandName, "Command Name should be initialized");
            Assert.IsNotNull(argumentHandler.GlobalFlags, "Global Flags should be initialized");
            Assert.IsNotNull(argumentHandler.CommandArgs, "Settings should be initialized");

            argumentHandler.HandleArgs(new string[] { "hello-world" });

            Assert.That(argumentHandler.CommandName, Is.EqualTo("hello-world"), "Command Name should be set to \"hello-world\"");
            Assert.That(argumentHandler.GlobalFlags.Count, Is.EqualTo(0), "Global Flags should be empty");
            Assert.That(argumentHandler.CommandArgs.Count, Is.EqualTo(0), "Command Args should be empty");
        }

        /// <summary>
        /// Tests the HandleArgs method of the ArgumentHandler class with global flags and command name.
        /// </summary>
        [Test]
        public void HandleArgsTestGlobalFlagsAndCommandName()
        {
            CommandFactory.LoadCommands();
            FlagFactory.LoadFlags();

            UnitTestSettings settings = new UnitTestSettings();

            Assert.IsNotNull(settings, "Settings should be initialized");

            ArgumentHandler argumentHandler = new ArgumentHandler(settings);

            Assert.IsNotNull(argumentHandler, "ArgumentHandler should be initialized");
            Assert.IsNotNull(argumentHandler.CommandName, "Command Name should be initialized");
            Assert.IsNotNull(argumentHandler.GlobalFlags, "Global Flags should be initialized");
            Assert.IsNotNull(argumentHandler.CommandArgs, "Settings should be initialized");

            argumentHandler.HandleArgs(new string[] { "--verbose", "hello-world" });

            Assert.That(argumentHandler.CommandName, Is.EqualTo("hello-world"), "Command Name should be set to \"hello-world\"");
            Assert.That(argumentHandler.GlobalFlags.Count, Is.EqualTo(1), "Global Flags should be empty");
            Assert.That(argumentHandler.CommandArgs.Count, Is.EqualTo(0), "Command Args should be empty");

            argumentHandler = new ArgumentHandler(settings);

            Assert.IsNotNull(argumentHandler, "ArgumentHandler should be initialized");
            Assert.IsNotNull(argumentHandler.CommandName, "Command Name should be initialized");
            Assert.IsNotNull(argumentHandler.GlobalFlags, "Global Flags should be initialized");
            Assert.IsNotNull(argumentHandler.CommandArgs, "Settings should be initialized");

            argumentHandler.HandleArgs(new string[] { "--verbose", "--non-verbose", "hello-world" });

            Assert.That(argumentHandler.CommandName, Is.EqualTo("hello-world"), "Command Name should be set to \"hello-world\"");
            Assert.That(argumentHandler.GlobalFlags.Count, Is.EqualTo(2), "Global Flags should be empty");
            Assert.That(argumentHandler.CommandArgs.Count, Is.EqualTo(0), "Command Args should be empty");

            argumentHandler = new ArgumentHandler(settings);

            Assert.IsNotNull(argumentHandler, "ArgumentHandler should be initialized");
            Assert.IsNotNull(argumentHandler.CommandName, "Command Name should be initialized");
            Assert.IsNotNull(argumentHandler.GlobalFlags, "Global Flags should be initialized");
            Assert.IsNotNull(argumentHandler.CommandArgs, "Settings should be initialized");

            argumentHandler.HandleArgs(new string[] { "--verbose", "true", "--non-verbose", "false", "hello-world" });

            Assert.That(argumentHandler.CommandName, Is.EqualTo("hello-world"), "Command Name should be set to \"hello-world\"");
            Assert.That(argumentHandler.GlobalFlags.Count, Is.EqualTo(2), "Global Flags should be empty");
            Assert.That(argumentHandler.CommandArgs.Count, Is.EqualTo(0), "Command Args should be empty");

            Assert.That(argumentHandler.GlobalFlags.First().Value.Name, Is.EqualTo("verbose"), "Global Flag Name should be set to \"verbose\"");
            Assert.That(argumentHandler.GlobalFlags.First().Value.ShorthandName, Is.EqualTo("v"), "Global Flag Shorthand Name should be set to \"v\"");
            Assert.That(argumentHandler.GlobalFlags.First().Value.Arguments.Count, Is.EqualTo(1), "Global Flag Arguments should be empty");
            Assert.That(argumentHandler.GlobalFlags.First().Value.Arguments.First(), Is.EqualTo("true"), "Global Flag Argument 0 should be set to \"true\"");

            Assert.That(argumentHandler.GlobalFlags.Last().Value.Name, Is.EqualTo("non-verbose"), "Global Flag Name should be set to \"non-verbose\"");
            Assert.That(argumentHandler.GlobalFlags.Last().Value.ShorthandName, Is.EqualTo("nv"), "Global Flag Shorthand Name should be set to \"nv\"");
            Assert.That(argumentHandler.GlobalFlags.Last().Value.Arguments.Count, Is.EqualTo(1), "Global Flag Arguments should be empty");
            Assert.That(argumentHandler.GlobalFlags.Last().Value.Arguments.First(), Is.EqualTo("false"), "Global Flag Argument 0 should be set to \"false\"");
        }

        /// <summary>
        /// Tests the HandleArgs method of the ArgumentHandler class with command arguments.
        /// </summary>
        [Test]
        public void HandleArgsTestCommandArgs()
        {
            CommandFactory.LoadCommands();
            FlagFactory.LoadFlags();

            UnitTestSettings settings = new UnitTestSettings();

            Assert.IsNotNull(settings, "Settings should be initialized");

            ArgumentHandler argumentHandler = new ArgumentHandler(settings);

            Assert.IsNotNull(argumentHandler, "ArgumentHandler should be initialized");
            Assert.IsNotNull(argumentHandler.CommandName, "Command Name should be initialized");
            Assert.IsNotNull(argumentHandler.GlobalFlags, "Global Flags should be initialized");
            Assert.IsNotNull(argumentHandler.CommandArgs, "Settings should be initialized");

            argumentHandler.HandleArgs(new string[] { "hello-world", "name" });

            Assert.That(argumentHandler.CommandName, Is.EqualTo("hello-world"), "Command Name should be set to \"hello-world\"");
            Assert.That(argumentHandler.GlobalFlags.Count, Is.EqualTo(0), "Global Flags should be empty");
            Assert.That(argumentHandler.CommandArgs.Count, Is.EqualTo(1), "Command Args should be empty");
            Assert.That(argumentHandler.CommandArgs[0], Is.EqualTo("name"), "Command Args Key should be set to \"name\"");
        }

        /// <summary>
        /// Tests the HandleArgs method with all combinations of command name, global flags, and command arguments.
        /// </summary>
        [Test]
        public void HandleArgsTestAll()
        {
            CommandFactory.LoadCommands();
            FlagFactory.LoadFlags();

            UnitTestSettings settings = new UnitTestSettings();

            Assert.IsNotNull(settings, "Settings should be initialized");

            ArgumentHandler argumentHandler = new ArgumentHandler(settings);

            Assert.IsNotNull(argumentHandler, "ArgumentHandler should be initialized");
            Assert.IsNotNull(argumentHandler.CommandName, "Command Name should be initialized");
            Assert.IsNotNull(argumentHandler.GlobalFlags, "Global Flags should be initialized");
            Assert.IsNotNull(argumentHandler.CommandArgs, "Settings should be initialized");

            argumentHandler.HandleArgs(new string[] { "--verbose", "true", "--non-verbose", "false", "hello-world", "name" });

            Assert.That(argumentHandler.CommandName, Is.EqualTo("hello-world"), "Command Name should be set to \"hello-world\"");
            Assert.That(argumentHandler.GlobalFlags.Count, Is.EqualTo(2), "Global Flags should be empty");
            Assert.That(argumentHandler.CommandArgs.Count, Is.EqualTo(1), "Command Args should be empty");
            Assert.That(argumentHandler.CommandArgs[0], Is.EqualTo("name"), "Command Args Key should be set to \"name\"");

            Assert.That(argumentHandler.GlobalFlags.First().Value.Name, Is.EqualTo("verbose"), "Global Flag Name should be set to \"verbose\"");
            Assert.That(argumentHandler.GlobalFlags.First().Value.ShorthandName, Is.EqualTo("v"), "Global Flag Shorthand Name should be set to \"v\"");
            Assert.That(argumentHandler.GlobalFlags.First().Value.Arguments.Count, Is.EqualTo(1), "Global Flag Arguments should be empty");
            Assert.That(argumentHandler.GlobalFlags.First().Value.Arguments.First(), Is.EqualTo("true"), "Global Flag Argument 0 should be set to \"true\"");

            Assert.That(argumentHandler.GlobalFlags.Last().Value.Name, Is.EqualTo("non-verbose"), "Global Flag Name should be set to \"non-verbose\"");
            Assert.That(argumentHandler.GlobalFlags.Last().Value.ShorthandName, Is.EqualTo("nv"), "Global Flag Shorthand Name should be set to \"nv\"");
            Assert.That(argumentHandler.GlobalFlags.Last().Value.Arguments.Count, Is.EqualTo(1), "Global Flag Arguments should be empty");
            Assert.That(argumentHandler.GlobalFlags.Last().Value.Arguments.First(), Is.EqualTo("false"), "Global Flag Argument 0 should be set to \"false\"");
        }
    }
}