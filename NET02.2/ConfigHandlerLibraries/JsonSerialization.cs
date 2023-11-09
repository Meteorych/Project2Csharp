using System.Collections.ObjectModel;
using System.Text.Json;
using ConfigHandlerLibraries.LoginClasses;

namespace ConfigHandlerLibraries
{
    public static class JsonSerialization
    {
        /// <summary>
        /// Default values for attributes during JsonSerialization.
        /// </summary>
        private static readonly ReadOnlyDictionary<string, string> DefaultValues = new(new Dictionary<string, string>
        {
            { "top", "0" },
            { "left", "0" },
            { "width", "400" },
            { "height", "150" },
        });

        /// <summary>
        /// Method for JSON serialization of Logins config.
        /// </summary>
        /// <param name="obj"></param>
        public static void ConfigSerialization(object obj)
        {
            if (obj is not LoginsConfig logins) return;
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
                var jsonString = JsonSerializer.Serialize(login, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, jsonString);
            }

        }

    }
}
