using NanoDNA.CLIFramework.Flags;
using NanoDNA.CLIFramework.Tests.Application;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace NanoDNA.CLIFramework.Tests.Tests
{
    /// <summary>
    /// Tests the Command class.
    /// </summary>
    internal class CommandTests
    {
        /// <summary>
        /// Tests the Initialization of the Command class.
        /// </summary>
        [Test]
        public void CommandInitializationTest()
        {
            UnitTestSettings settings = new UnitTestSettings();
            Dictionary<Type, Flag> globalFlags = new Dictionary<Type, Flag>();

            UnitTestDataManager dataManager = new UnitTestDataManager(settings, globalFlags);

            Assert.IsNotNull(settings, "Settings should be initialized");
            Assert.IsNotNull(globalFlags, "Global Flags should be initialized");
            Assert.IsNotNull(dataManager, "DataManager should be initialized");

            HelloWorld helloWorld = new HelloWorld(dataManager);

            Assert.IsNotNull(helloWorld, "Command should be initialized");
            Assert.That(helloWorld.Name, Is.EqualTo("hello-world"), "Command Name should be set to \"hello-world\"");
            Assert.That(helloWorld.Description, Is.EqualTo("Says Hello World! in the Console"), "Command Description should be set to \"Says Hello World! in the Console\"");
            Assert.That(helloWorld.DataManager, Is.EqualTo(dataManager), "Command DataManager should be set to the provided DataManager");
        }

        /// <summary>
        /// Tests the Execute method of the Command class.
        /// </summary>
        [Test]
        public void CommandExecuteTest()
        {
            UnitTestSettings settings = new UnitTestSettings();
            Dictionary<Type, Flag> globalFlags = new Dictionary<Type, Flag>();

            UnitTestDataManager dataManager = new UnitTestDataManager(settings, globalFlags);

            Assert.IsNotNull(settings, "Settings should be initialized");
            Assert.IsNotNull(globalFlags, "Global Flags should be initialized");
            Assert.IsNotNull(dataManager, "DataManager should be initialized");

            HelloWorld helloWorld = new HelloWorld(dataManager);

            Assert.IsNotNull(helloWorld, "Command should be initialized");

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                helloWorld.Execute(new string[] { });
                string result = sw.ToString().Trim();

                Assert.That(result, Is.EqualTo("Hello World!"), "Command should output \"Hello World!\"");
            }
        }
    }
}
