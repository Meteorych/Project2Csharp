using System.Collections.ObjectModel;
using System.Net;
using System.Text.Json;
using ConfigHandlerLibraries.LoginClasses;

namespace ConfigHandlerLibraries
{
    public static class JsonConfig
    {
        

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
                        window.Attributes = window.Attributes.ToDictionary(pair => pair.Key, pair => LoginsConfig.DefaultValues.ContainsKey(pair.Key) && pair.Value == "?" ? LoginsConfig.DefaultValues[pair.Key] : pair.Value);
                    });
                }
                var jsonString = JsonSerializer.Serialize(login, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, jsonString);
            }

        }
        public static List<Login> ConfigDeserialization(string jsonWay)
        {
            var info = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Config\", jsonWay));
            if (string.IsNullOrEmpty(info))
            {
                throw new ArgumentNullException(nameof(info), "Argument is null or empty!");
            }
            var result = JsonSerializer.Deserialize<List<Login>>(info);
            return result ?? throw new InvalidOperationException("This file can't be read using JsonSerializer.");
        }

    }
}
