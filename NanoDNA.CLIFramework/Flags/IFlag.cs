
namespace NanoDNA.CLIFramework.Flags
{
    /// <summary>
    /// Defines the Data Associated with a CLI Command Flag.
    /// </summary>
    internal interface IFlag
    {
        /// <summary>
        /// Name of the Flag, used to Call the Flag in the CLI.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Shorthand Name of the Flag, used to Call the Flag in the CLI.
        /// </summary>
        public string ShorthandName { get; }

        /// <summary>
        /// Description of the Flag, used to describe the Flag in the CLI. Displayed when the user calls the help command.
        /// </summary>
        public string Description { get; }
    }
}
