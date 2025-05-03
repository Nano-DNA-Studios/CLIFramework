using System;
using System.IO;
using NUnit.Framework;
using NanoDNA.CLIFramework.Tests.Application;

namespace NanoDNA.CLIFramework.Tests.Tests
{
    /// <summary>
    /// Tests the CLI Application class.
    /// </summary>
    internal class CLIAppTests
    {
        /// <summary>
        /// Tests if the CLI application is initialized correctly.
        /// </summary>
        [Test]
        public void TestCLIAppInitialization()
        {
            UnitTestCLI cliApplication = new UnitTestCLI();

            Assert.IsNotNull(cliApplication);
            Assert.IsNotNull(cliApplication.Name);
            Assert.IsNotNull(cliApplication.Settings);
            Assert.IsNotNull(cliApplication.ArgumentHandler);

            Assert.IsNull(cliApplication.DataManager);

            Assert.That(cliApplication.Name, Is.EqualTo("UnitTestCLI"));
        }

        /// <summary>
        /// Tests if the Command is being properly executed.
        /// </summary>
        [Test]
        public void CLIApplicationRun()
        {
            using (StringWriter sw = new StringWriter())
            {
                UnitTestCLI cliApplication = new UnitTestCLI();

                Console.SetOut(sw);

                cliApplication.Run(new string[] { "hello-world" });

                string result = sw.ToString().Trim();

                Assert.That(result, Is.EqualTo("Hello World!"), "Command should output \"Hello World!\"");
            }

            using (StringWriter sw = new StringWriter())
            {
                UnitTestCLI cliApplication = new UnitTestCLI();

                Console.SetOut(sw);

                cliApplication.Run(new string[] { "--verbose", "hello-world" });

                string result = sw.ToString().Trim();

                if (OperatingSystem.IsWindows())
                    Assert.That(result, Is.EqualTo("Hello World!\r\nVERBOSE!"), "Command should output \"Hello World!\"");
                else
                    Assert.That(result, Is.EqualTo("Hello World!\nVERBOSE!"), "Command should output \"Hello World!\"");
            }

            using (StringWriter sw = new StringWriter())
            {
                UnitTestCLI cliApplication = new UnitTestCLI();

                Console.SetOut(sw);

                cliApplication.Run(new string[] { "--verbose", "--non-verbose", "hello-world" });

                string result = sw.ToString().Trim();

                if (OperatingSystem.IsWindows())
                    Assert.That(result, Is.EqualTo("Hello World!\r\nVERBOSE!\r\nNON-VERBOSE!"), "Command should output \"Hello World!\"");
                else
                    Assert.That(result, Is.EqualTo("Hello World!\nVERBOSE!\nNON-VERBOSE!"), "Command should output \"Hello World!\"");
            }

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }

        /// <summary>
        /// Tests if the CLI Application throws an exception when the command does not exist.
        /// </summary>
        [Test]
        public void CLIAppicationRunFail()
        {
            UnitTestCLI cliApplication = new UnitTestCLI();

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                Assert.Throws<Exception>(() => cliApplication.Run(new string[] { "hello" }));
            }

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }
    }
}