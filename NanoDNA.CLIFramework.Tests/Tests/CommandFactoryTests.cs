using System;
using System.IO;
using NUnit.Framework;
using System.Collections.Generic;
using NanoDNA.CLIFramework.Flags;
using NanoDNA.CLIFramework.Commands;
using NanoDNA.CLIFramework.Tests.Application;

namespace NanoDNA.CLIFramework.Tests.Tests
{
    /// <summary>
    /// Tests the CommandFactory class.
    /// </summary>
    internal class CommandFactoryTests
    {
        /// <summary>
        /// Tests the CommandFactory to ensure that it can load commands correctly.
        /// </summary>
        [Test]
        public void CommandExistsFailTest()
        {
            CommandFactory.LoadCommands();

            Assert.That(CommandFactory.CommandExists("hello"), Is.False);
            Assert.That(CommandFactory.CommandExists("hello-wor"), Is.False);

        }

        /// <summary>
        /// Tests the CommandFactory to ensure that it can load commands correctly.
        /// </summary>
        [Test]
        public void CommandExistsPassTest()
        {
            CommandFactory.LoadCommands();

            Assert.That(CommandFactory.CommandExists("hello-world"), Is.True);
        }

        /// <summary>
        /// Tests the GetCommand method of the CommandFactory.
        /// </summary>
        [Test]
        public void GetCommandTest()
        {
            UnitTestSettings settings = new UnitTestSettings();
            Dictionary<Type, Flag> globalFlags = new Dictionary<Type, Flag>();

            UnitTestDataManager dataManager = new UnitTestDataManager(settings, globalFlags);

            Assert.IsNotNull(settings, "Settings should be initialized");
            Assert.IsNotNull(globalFlags, "Global Flags should be initialized");
            Assert.IsNotNull(dataManager, "DataManager should be initialized");

            CommandFactory.LoadCommands();

            HelloWorld helloworld = (HelloWorld)CommandFactory.GetCommand("hello-world", dataManager);

            Assert.IsNotNull(helloworld, "Command should be defined");
            Assert.That(helloworld.Name, Is.EqualTo("hello-world"), "Command Name should be set to \"hello-world\"");
            Assert.That(helloworld.Description, Is.EqualTo("Says Hello World! in the Console"), "Command Description should be set to \"Says Hello World! in the Console\"");
            Assert.That(helloworld.DataManager, Is.EqualTo(dataManager), "Command DataManager should be set to the provided DataManager");
        }

        /// <summary>
        /// Tests if the GetCommand Function throws an exception when the command does not exist.
        /// </summary>
        [Test]
        public void GetCommandFailTest()
        {
            UnitTestSettings settings = new UnitTestSettings();
            Dictionary<Type, Flag> globalFlags = new Dictionary<Type, Flag>();

            UnitTestDataManager dataManager = new UnitTestDataManager(settings, globalFlags);

            Assert.IsNotNull(settings, "Settings should be initialized");
            Assert.IsNotNull(globalFlags, "Global Flags should be initialized");
            Assert.IsNotNull(dataManager, "DataManager should be initialized");

            CommandFactory.LoadCommands();

            Assert.Throws<Exception>(() => CommandFactory.GetCommand("hello", dataManager));
        }

        /// <summary>
        /// Tests the Execution of a Command
        /// </summary>
        [Test]
        public void ExecuteCommandTest()
        {
            UnitTestSettings settings = new UnitTestSettings();
            Dictionary<Type, Flag> globalFlags = new Dictionary<Type, Flag>();

            UnitTestDataManager dataManager = new UnitTestDataManager(settings, globalFlags);

            Assert.IsNotNull(settings, "Settings should be initialized");
            Assert.IsNotNull(globalFlags, "Global Flags should be initialized");
            Assert.IsNotNull(dataManager, "DataManager should be initialized");

            CommandFactory.LoadCommands();

            HelloWorld helloworld = (HelloWorld)CommandFactory.GetCommand("hello-world", dataManager);

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });

            try
            {
                helloworld.Execute(new string[] { "" });
            }
            catch (Exception ex)
            {
                Assert.Fail($"Command Execution failed with Exception: {ex.Message}");
            }
        }
    }
}