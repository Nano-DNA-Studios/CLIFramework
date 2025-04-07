using NanoDNA.CLIFramework.Flags;

namespace NanoDNA.CLIFramework.Tests.Application
{
    internal class Verbose : Flag
    {
        public Verbose(string[] arguments) : base(arguments)
        {
        }

        public override string Name => "verbose";

        public override string ShorthandName => "v";

        public override string Description => "Makes the CLI Application dispay more verbose information";
    }
}
