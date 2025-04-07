using NanoDNA.CLIFramework.Flags;

namespace NanoDNA.CLIFramework.Tests.Application
{
    internal class NonVerbose : Flag
    {
        public NonVerbose(string[] arguments) : base(arguments)
        {
        }

        public override string Name => "non-verbose";

        public override string ShorthandName => "nv";

        public override string Description => "Makes the CLI Application dispay less verbose information";
    }
}
