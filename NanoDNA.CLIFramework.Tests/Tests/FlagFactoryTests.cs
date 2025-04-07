using NanoDNA.CLIFramework.Flags;
using NanoDNA.CLIFramework.Tests.Application;
using NUnit.Framework;
using System;

namespace NanoDNA.CLIFramework.Tests.Tests
{
    /// <summary>
    /// Tests the FlagFactory class.
    /// </summary>
    internal class FlagFactoryTests
    {
        /// <summary>
        /// Tests if the FlagFactory detectets non existent flags before they are loaded.
        /// </summary>
        [Test]
        public void FlagsExistsFailTests()
        {
            FlagFactory.LoadFlags();

            Assert.That(FlagFactory.FlagExists("test"), Is.False, "Flag should not exist");
            Assert.That(FlagFactory.FlagExists("t"), Is.False, "Flag should not exist");
        }

        /// <summary>
        /// Tests if the FlagFactory detects existent flags after they are loaded.
        /// </summary>
        [Test]
        public void FlagsExistsSuccessTests()
        {
            FlagFactory.LoadFlags();

            Assert.That(FlagFactory.FlagExists("verbose"), Is.True, "Flag should exist");
            Assert.That(FlagFactory.FlagExists("v"), Is.True, "Flag should exist");
            Assert.That(FlagFactory.FlagExists("non-verbose"), Is.True, "Flag should exist");
            Assert.That(FlagFactory.FlagExists("nv"), Is.True, "Flag should exist");
        }

        /// <summary>
        /// Tests the GetFlag method of the FlagFactory.
        /// </summary>
        [Test]
        public void GetFlagTests()
        {
            FlagFactory.LoadFlags();

            Verbose verbose = (Verbose)FlagFactory.GetFlag("verbose", ["true"]);

            Assert.IsNotNull(verbose, "Flag should be initialized");
            Assert.IsNotNull(verbose.Name, "Flag name should be initialized");
            Assert.IsNotNull(verbose.ShorthandName, "Flag shorthand name should be initialized");
            Assert.IsNotNull(verbose.Description, "Flag description should be initialized");
            Assert.IsNotNull(verbose.Arguments, "Flag arguments should be initialized");

            Assert.That(verbose.Arguments, Is.Not.Null, "Flag arguments should not be null");
            Assert.That(verbose.Arguments, Is.Not.Empty, "Flag arguments should not be empty");
            Assert.That(verbose.Arguments.Length, Is.EqualTo(1), "Flag arguments should contain 1 argument");
            Assert.That(verbose.Arguments[0], Is.EqualTo("true"), "Flag argument should be 'true'");

            NonVerbose nonVerbose = (NonVerbose)FlagFactory.GetFlag("non-verbose", ["false"]);

            Assert.IsNotNull(nonVerbose, "Flag should be initialized");
            Assert.IsNotNull(nonVerbose.Name, "Flag name should be initialized");
            Assert.IsNotNull(nonVerbose.ShorthandName, "Flag shorthand name should be initialized");
            Assert.IsNotNull(nonVerbose.Description, "Flag description should be initialized");
            Assert.IsNotNull(nonVerbose.Arguments, "Flag arguments should be initialized");

            Assert.That(nonVerbose.Arguments, Is.Not.Null, "Flag arguments should not be null");
            Assert.That(nonVerbose.Arguments, Is.Not.Empty, "Flag arguments should not be empty");
            Assert.That(nonVerbose.Arguments.Length, Is.EqualTo(1), "Flag arguments should contain 1 argument");
            Assert.That(nonVerbose.Arguments[0], Is.EqualTo("false"), "Flag argument should be 'false'");
        }

        /// <summary>
        /// Tests the GetFlag method of the FlagFactory with shorthand flags.
        /// </summary>
        [Test]
        public void GetShorthandFlagTest()
        {
            FlagFactory.LoadFlags();

            Verbose verbose = (Verbose)FlagFactory.GetFlag("v", ["true"]);

            Assert.IsNotNull(verbose, "Flag should be initialized");
            Assert.IsNotNull(verbose.Name, "Flag name should be initialized");
            Assert.IsNotNull(verbose.ShorthandName, "Flag shorthand name should be initialized");
            Assert.IsNotNull(verbose.Description, "Flag description should be initialized");
            Assert.IsNotNull(verbose.Arguments, "Flag arguments should be initialized");

            Assert.That(verbose.Arguments, Is.Not.Null, "Flag arguments should not be null");
            Assert.That(verbose.Arguments, Is.Not.Empty, "Flag arguments should not be empty");
            Assert.That(verbose.Arguments.Length, Is.EqualTo(1), "Flag arguments should contain 1 argument");
            Assert.That(verbose.Arguments[0], Is.EqualTo("true"), "Flag argument should be 'true'");

            NonVerbose nonVerbose = (NonVerbose)FlagFactory.GetFlag("nv", ["false"]);

            Assert.IsNotNull(nonVerbose, "Flag should be initialized");
            Assert.IsNotNull(nonVerbose.Name, "Flag name should be initialized");
            Assert.IsNotNull(nonVerbose.ShorthandName, "Flag shorthand name should be initialized");
            Assert.IsNotNull(nonVerbose.Description, "Flag description should be initialized");
            Assert.IsNotNull(nonVerbose.Arguments, "Flag arguments should be initialized");

            Assert.That(nonVerbose.Arguments, Is.Not.Null, "Flag arguments should not be null");
            Assert.That(nonVerbose.Arguments, Is.Not.Empty, "Flag arguments should not be empty");
            Assert.That(nonVerbose.Arguments.Length, Is.EqualTo(1), "Flag arguments should contain 1 argument");
            Assert.That(nonVerbose.Arguments[0], Is.EqualTo("false"), "Flag argument should be 'false'");
        }

        /// <summary>
        /// Tests if the GetFlag method of the FlagFactory throws an exception if the flag does not exist.
        /// </summary>
        [Test]
        public void GetFlagFailTest()
        {
            FlagFactory.LoadFlags();

            Assert.Throws<Exception>(() => FlagFactory.GetFlag("test", ["true"]));
            Assert.Throws<Exception>(() => FlagFactory.GetFlag("t", ["true"]));
        }
    }
}
