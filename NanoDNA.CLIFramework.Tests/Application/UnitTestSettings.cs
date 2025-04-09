using NanoDNA.CLIFramework.Data;

namespace NanoDNA.CLIFramework.Tests.Application
{
    internal class UnitTestSettings : Setting
    {
        public override string ApplicationName => "UnitTestCLI";

        public override string GlobalFlagPrefix => DEFAULT_GLOBAL_FLAG_PREFIX;

        public override string GlobalShorthandFlagPrefix => DEFAULT_GLOBAL_SHORTHAND_FLAG_PREFIX;

        /// <summary>
        /// Testing Property for the Settings Class
        /// </summary>
        public int TestInt { get; set; } = 0;
    }
}
