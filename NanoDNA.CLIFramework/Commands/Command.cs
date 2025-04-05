using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoDNA.CLIFramework.Commands
{
    internal class Command : ICommand
    {
        public string Name => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public void Execute(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
