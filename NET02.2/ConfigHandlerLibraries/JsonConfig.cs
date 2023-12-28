using System.Text.Json;
using ConfigHandlerLibraries.LoginClasses;

namespace ConfigHandlerLibraries
{
    public class JsonConfig : IConfigurable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uploadFileName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public List<Login> LoginsDataUpload(string uploadFileName)
        {
            var info = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"..\..\..\Config\", uploadFileName));
            if (string.IsNullOrEmpty(info))
            {
                throw new ArgumentNullException(nameof(info), "Argument is null or empty!");
            }
            var result = JsonSerializer.Deserialize<List<Login>>(info);
            return result ?? throw new InvalidOperationException("This file can't be read using JsonSerializer.");
        }

        /// <summary>
        /// Method for JSON serialization of Logins config.
        /// </summary>
        /// <param name="config"></param>
        public void LoginsDataDump(LoginsConfig? config)
        {
            if (config is null) return;
            foreach (var login in config)
            {
                var path = (Path.Combine(Environment.CurrentDirectory, @$"..\..\..\Config\{login.Name}.json"));
                login.LoginNullToDefault();
                var jsonString = JsonSerializer.Serialize(login, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, jsonString);
            }
        }
    }
}
