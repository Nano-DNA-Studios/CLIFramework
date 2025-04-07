using NanoDNA.CLIFramework.Tests.Application;
using NUnit.Framework;

namespace NanoDNA.CLIFramework.Tests.Tests
{
    /// <summary>
    /// Tests the Flag class.
    /// </summary>
    internal class FlagTests
    {
        /// <summary>
        /// Tests the Initialization of the Verbose flag.
        /// </summary>
        [Test]
        public void FlagInitializationTest()
        {
            Verbose flag = new Verbose(new string[] { "true" });

            Assert.IsNotNull(flag, "Flag should be initialized");
            Assert.IsNotNull(flag.Arguments, "Flag arguments should be initialized");
            Assert.IsNotNull(flag.Name, "Flag name should be initialized");
            Assert.IsNotNull(flag.ShorthandName, "Flag shorthand name should be initialized");
            Assert.IsNotNull(flag.Description, "Flag description should be initialized");


            Assert.That(flag.Name, Is.EqualTo("verbose"), "Flag name should be 'verbose'");
            Assert.That(flag.ShorthandName, Is.EqualTo("v"), "Flag shorthand name should be 'v'");
            Assert.That(flag.Description, Is.EqualTo("Makes the CLI Application dispay more verbose information"), "Flag description should be 'Makes the CLI Application dispay more verbose information'");

            Assert.That(flag.Arguments, Is.Not.Null, "Flag arguments should not be null");
            Assert.That(flag.Arguments, Is.Not.Empty, "Flag arguments should not be empty");
            Assert.That(flag.Arguments.Length, Is.EqualTo(1), "Flag arguments should contain 1 argument");
            Assert.That(flag.Arguments[0], Is.EqualTo("true"), "Flag argument should be 'true'");
        }
    }
}