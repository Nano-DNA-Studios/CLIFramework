using System.IO;
using NUnit.Framework;
using System.Reflection;
using NanoDNA.CLIFramework.Data;
using NanoDNA.CLIFramework.Tests.Application;
using System;

namespace NanoDNA.CLIFramework.Tests.Tests
{
    /// <summary>
    /// Unit Tests for the Settings Class
    /// </summary>
    internal class SettingsTests
    {
        /// <summary>
        /// Tests the Initialization of the Settings Class
        /// </summary>
        [Test]
        public void SettingsInitialization()
        {
            Setting settings = new UnitTestSettings();

            Assert.IsNotNull(settings, "Settings should be set to a Value");
        }

        /// <summary>
        /// Tests the Default Values of the Settings Class
        /// </summary>
        [Test]
        public void SettingsAssignedValuesTest()
        {
            Setting settings = new UnitTestSettings();

            Assert.IsNotNull(settings);

            Assert.That(settings.ApplicationName, Is.EqualTo("UnitTestCLI"), $"Application name should be set to \"UnitTestCLI\"");
            Assert.That(settings.GlobalFlagPrefix, Is.EqualTo(Setting.DEFAULT_GLOBAL_FLAG_PREFIX), "Global Flag Prefix is not the Default");
            Assert.That(settings.GlobalShorthandFlagPrefix, Is.EqualTo(Setting.DEFAULT_GLOBAL_SHORTHAND_FLAG_PREFIX), "Global Shorthand Flag Prefix is not the Default");
        }

        /// <summary>
        /// Tests the Application Path of the Settings Class
        /// </summary>
        [Test]
        public void SettingsApplicationPathTest()
        {
            Setting settings = new UnitTestSettings();

            Assert.IsNotNull(settings);

            Assert.That(settings.ApplicationPath, Is.Not.Null, "Application Path should not be null");
            Assert.That(settings.ApplicationPath, Is.Not.Empty, "Application Path should not be empty");
            Assert.That(settings.ApplicationPath, Is.EqualTo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)), "Application Path should be equivalent to the Path of the Assembly");
            Assert.That(Directory.Exists(settings.ApplicationPath), Is.True, $"Application Path Directory should exist at \"{settings.ApplicationPath}\"");
        }

        /// <summary>
        /// Tests the Cache Path of the Settings Class
        /// </summary>
        [Test]
        public void SettingsCachePathTest()
        {
            Setting settings = new UnitTestSettings();

            Assert.IsNotNull(settings);

            Assert.That(settings.CachePath, Is.Not.Null, "Cache Path should not be null");
            Assert.That(settings.CachePath, Is.Not.Empty, "Cache Path should not be empty");
            Assert.That(settings.CachePath, Is.EqualTo(Path.Combine(settings.ApplicationPath, "Cache")), $"Cache Path should be \"{settings.ApplicationPath}\" + \"Cache\"");
            Assert.That(Directory.Exists(settings.CachePath), Is.True, "Cache Directory should be created");
        }

        /// <summary>
        /// Tests the Default Global Flag Prefixes of the Settings Class
        /// </summary>
        [Test]
        public void SettingsDefaultFlagPrefixes()
        {
            Assert.That(Setting.DEFAULT_GLOBAL_FLAG_PREFIX, Is.EqualTo("--"), "Default Global Flag Prefix should be \"--\"");
            Assert.That(Setting.DEFAULT_GLOBAL_SHORTHAND_FLAG_PREFIX, Is.EqualTo("-"), "Default Global Shorthand Flag Prefix should be \"-\"");
        }

        /// <summary>
        /// Tests if the Settings File should exist and be Present
        /// </summary>
        [Test]
        public void SettingsFileExists ()
        {
            Assert.That(File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Cache", "Settings.json")), Is.True, "Settings File should exist");
        }

        /// <summary>
        /// Tests the Loading of the Settings File
        /// </summary>
        [Test]
        public void SettingsLoadFile ()
        {
            UnitTestSettings settings = new UnitTestSettings();

            int randomInt = Random.Shared.Next(0, 100);

            Console.WriteLine($"Random Int: {randomInt}");

            settings.TestInt = randomInt;
            settings.SaveSettings();

            UnitTestSettings loadedSettings = Setting.LoadSettings<UnitTestSettings>();

            Assert.That(loadedSettings.TestInt, Is.EqualTo(randomInt), "Loaded Settings should be equivalent to the Saved Settings");
        }
    }
}
