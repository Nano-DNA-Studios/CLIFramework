using NanoDNA.CLIFramework.Data;
using NanoDNA.CLIFramework.Flags;
using System;
using System.Collections.Generic;

namespace NanoDNA.CLIFramework.Tests.Application
{
    internal class UnitTestDataManager : DataManager
    {
        public UnitTestDataManager(Setting settings, Dictionary<Type, Flag> globalFlags) : base(settings, globalFlags)
        {
        }
    }
}
