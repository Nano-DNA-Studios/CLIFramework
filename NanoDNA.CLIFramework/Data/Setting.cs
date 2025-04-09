using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace NanoDNA.CLIFramework.Data
{
    /// <summary>
    /// Defines the Settings for a CLI Application.
    /// </summary>
    public abstract class Setting : ISetting
    {
        /// <summary>
        /// The Default Global Flag Prefix for the <see cref="CLIApplication{S, DM}"/>. Used by most CLI Applications.
        /// </summary>
        public const string DEFAULT_GLOBAL_FLAG_PREFIX = "--";

        /// <summary>
        /// The Default Global Shorthand Flag Prefix for the <see cref="CLIApplication{S, DM}"/>.. Used by most CLI Applications.
        /// </summary>
        public const string DEFAULT_GLOBAL_SHORTHAND_FLAG_PREFIX = "-";

        /// <inheritdoc/>
        public abstract string ApplicationName { get; }

        /// <inheritdoc/>
        public string ApplicationPath { get; }

        /// <inheritdoc/>
        public abstract string GlobalFlagPrefix { get; }

        /// <inheritdoc/>
        public abstract string GlobalShorthandFlagPrefix { get; }

        /// <inheritdoc/>
        public string CachePath { get; protected set; }
        
        /// <inheritdoc/>
        public string SettingsPath { get; protected set; }

        /// <summary>
        /// Initializes a new Instance of a <see cref="Setting"/>.
        /// </summary>
        public Setting()
        {
            ApplicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            CachePath = Path.Combine(ApplicationPath, "Cache");
            SettingsPath = Path.Combine(CachePath, "Settings.json");

            if (!Directory.Exists(CachePath))
                Directory.CreateDirectory(CachePath);

            Console.WriteLine($"Cache Path: {CachePath}");
        }

        /// <inheritdoc/>
        public static T LoadSettings<T> () where T : Setting, new()
        {
            T settings = new T();

            if (!File.Exists(settings.SettingsPath))
            {
                File.WriteAllText(settings.SettingsPath, JsonConvert.SerializeObject(settings, Formatting.Indented));
                return settings;
            }
            else
            {
                string json = File.ReadAllText(settings.SettingsPath);
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        /// <inheritdoc/>
        public void SaveSettings()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(SettingsPath, json);
        }
    }
}