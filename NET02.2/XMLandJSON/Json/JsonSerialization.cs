using System;
using System.Text.Json;
using System.Xml;
using XMLandJSON.LoginClasses;

namespace XMLandJSON.Json
{
    public abstract class JsonSerialization
    {
        /// <summary>
        /// Default values for attributes during JsonSerialization.
        /// </summary>
        private static readonly Dictionary<string, string> DefaultValues = new()
        {
            { "top", "0" },
            { "left", "0" },
            { "width", "400" },
            { "height", "400" },
        };
        /// <summary>
        /// Method for JSON serialization of Logins config.
        /// </summary>
        /// <param name="logins"></param>
        public static void ConfigSerialization(LoginsConfig logins)
        {
            foreach (var login in logins)
            {
                var path = (Path.Combine(Environment.CurrentDirectory, @$"..\..\..\Config\{login.Name}.json"));
                if (login.Windows.Any(window => window.Attributes.ContainsValue("?")))
                {
                    login.Windows.ForEach(window =>
                    {
                        window.Attributes = window.Attributes.ToDictionary(pair => pair.Key, pair => DefaultValues.ContainsKey(pair.Key) && pair.Value == "?" ? DefaultValues[pair.Key] : pair.Value);
                    });
                }
                File.WriteAllText(path, JsonSerializer.Serialize(login, new JsonSerializerOptions { WriteIndented = true }) );
            }
        }

    }
}
