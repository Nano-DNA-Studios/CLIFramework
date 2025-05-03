using System;
using NanoDNA.CLIFramework.Data;
using NanoDNA.CLIFramework.Commands;

namespace NanoDNA.CLIFramework.Tests.Application
{
    internal class HelloWorld : Command
    {
        public HelloWorld(IDataManager dataManager) : base(dataManager)
        {
        }

        public override string Name => "hello-world";

        public override string Description => "Says Hello World! in the Console";

        public override void Execute(string[] args)
        {
            Console.WriteLine("Hello World!");

            if (HasFlag<Verbose>())
                Console.WriteLine("VERBOSE!");

            if (HasFlag<NonVerbose>())
                Console.WriteLine("NON-VERBOSE!");
        }
    }
}
