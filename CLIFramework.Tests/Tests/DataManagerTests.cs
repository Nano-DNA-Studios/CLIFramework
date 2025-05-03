using System;
using System.IO;
using NUnit.Framework;
using NanoDNA.CLIFramework.Flags;
using System.Collections.Generic;
using NanoDNA.CLIFramework.Tests.Application;

namespace NanoDNA.CLIFramework.Tests.Tests
{
    /// <summary>
    /// Tests the DataManager class.
    /// </summary>
    internal class DataManagerTests
    {
        /// <summary>
        /// Tests the initialization of the DataManager.
        /// </summary>
        [Test]
        public void DataManagerInitializationTest()
        {
            UnitTestSettings settings = new UnitTestSettings();
            Dictionary<Type, Flag> globalFlags = new Dictionary<Type, Flag>();

            UnitTestDataManager dataManager = new UnitTestDataManager(settings, globalFlags);

            Assert.IsNotNull(settings, "Settings should be initialized");
            Assert.IsNotNull(globalFlags, "Global Flags should be initialized");
            Assert.IsNotNull(dataManager, "DataManager should be initialized");

            Assert.That(dataManager.Settings.GetType(), Is.EqualTo(settings.GetType()), "Settings should be set to the provided settings type");
            Assert.That(dataManager.Settings, Is.EqualTo(settings), "Settings should be set to the provided settings");
            Assert.That(dataManager.GlobalFlags, Is.EqualTo(globalFlags), "Global Flags should be set to the provided global flags");
        }

        /// <summary>
        /// Tests the CWD (Current Working Directory) of the DataManager.
        /// </summary>
        [Test]
        public void DataManagerCurrentWorkingDirectoryTest()
        {
            UnitTestSettings settings = new UnitTestSettings();
            Dictionary<Type, Flag> globalFlags = new Dictionary<Type, Flag>();

            UnitTestDataManager dataManager = new UnitTestDataManager(settings, globalFlags);

            Assert.IsNotNull(dataManager, "DataManager should be initialized");

            string currentDirectory = Environment.CurrentDirectory;

            Assert.That(dataManager.CWD, Is.Not.Null, "Current Working Directory should not be null");
            Assert.That(dataManager.CWD, Is.Not.Empty, "Current Working Directory should not be empty");
            Assert.That(dataManager.CWD, Is.EqualTo(currentDirectory), "Current Working Directory should be set to the current directory");

            Assert.That(dataManager.CWDCachePath, Is.Not.Null, "Current Working Directory Cache Path should not be null");
            Assert.That(dataManager.CWDCachePath, Is.Not.Empty, "Current Working Directory Cache Path should not be empty");
            Assert.That(dataManager.CWDCachePath, Is.EqualTo(Path.Combine(dataManager.CWD, $"{settings.ApplicationName}Cache")), "Current Working Directory Cache Path should be set to the Cache Path + \"CWD\"");
        }

        /// <summary>
        /// Tests the Global Flags of the DataManager.
        /// </summary>
        [Test]
        public void DataManagerGlobalFlagsTest()
        {
            UnitTestSettings settings = new UnitTestSettings();
            Dictionary<Type, Flag> globalFlags = new Dictionary<Type, Flag>() { { typeof(Verbose), new Verbose(["arg1", "arg2"]) } };

            UnitTestDataManager dataManager = new UnitTestDataManager(settings, globalFlags);

            Assert.IsNotNull(dataManager, "DataManager should be initialized");

            Assert.That(dataManager.GlobalFlags, Is.Not.Null, "Global Flags should not be null");
            Assert.That(dataManager.GlobalFlags, Is.Not.Empty, "Global Flags should not be empty");
            Assert.That(dataManager.GlobalFlags, Is.EqualTo(globalFlags), "Global Flags should be set to the provided global flags");
            Assert.That(dataManager.GlobalFlags.Count, Is.EqualTo(1), "Global Flags should contain 1 Flag");
            Assert.That(dataManager.GlobalFlags[typeof(Verbose)], Is.Not.Null, "Global Flag should not be null");
            Assert.That(dataManager.GlobalFlags[typeof(Verbose)], Is.EqualTo(globalFlags[typeof(Verbose)]), "Global Flag should be set to the provided global flag");
            Assert.That(dataManager.GlobalFlags[typeof(Verbose)].Name, Is.EqualTo("verbose"), "Global Flag Name should be set to \"verbose\"");
            Assert.That(dataManager.GlobalFlags[typeof(Verbose)].ShorthandName, Is.EqualTo("v"), "Global Flag Shorthand Name should be set to \"v\"");
            Assert.That(dataManager.GlobalFlags[typeof(Verbose)].Description, Is.EqualTo("Makes the CLI Application dispay more verbose information"), "Global Flag Description should be set to \"Makes the CLI Application dispay more verbose information\"");
            Assert.That(dataManager.GlobalFlags[typeof(Verbose)].Arguments, Is.Not.Null, "Global Flag Arguments should not be null");
            Assert.That(dataManager.GlobalFlags[typeof(Verbose)].Arguments, Is.Not.Empty, "Global Flag Arguments should not be empty");
            Assert.That(dataManager.GlobalFlags[typeof(Verbose)].Arguments.Length, Is.EqualTo(2), "Global Flag Arguments should contain 2 Arguments");
            Assert.That(dataManager.GlobalFlags[typeof(Verbose)].Arguments[0], Is.EqualTo("arg1"), "Global Flag Argument 0 should be set to \"arg1\"");
            Assert.That(dataManager.GlobalFlags[typeof(Verbose)].Arguments[1], Is.EqualTo("arg2"), "Global Flag Argument 1 should be set to \"arg2\"");
        }

        /// <summary>
        /// Tests the HasFlag method of the DataManager.
        /// </summary>
        [Test]
        public void DataManagerHasFlags()
        {
            UnitTestSettings settings = new UnitTestSettings();
            Dictionary<Type, Flag> globalFlags = new Dictionary<Type, Flag>() { { typeof(Verbose), new Verbose(["arg1", "arg2"]) } };

            UnitTestDataManager dataManager = new UnitTestDataManager(settings, globalFlags);

            Assert.IsNotNull(dataManager, "DataManager should be initialized");

            Assert.That(dataManager.HasFlag<Verbose>(), Is.True, "DataManager should have the Verbose Flag");
            Assert.That(dataManager.HasFlag<NonVerbose>(), Is.False, "DataManager should not have the NonVerbose Flag");
        }
    }
}