using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Assembly_and_Metadata
{
    public class ConfigurationOptions : ILogConfigurable
    {
        private readonly string _filePath;
        private LogLevels _minimumLogLevel;

        public LogLevels GetLogMinimumLevel => _minimumLogLevel;

        public ConfigurationOptions(string filePath)
        {
            _filePath = filePath;
        }

        private void ReadConf()
        {
            var config = JsonSerializer.Deserialize<Dictionary<string, string>>(_filePath);
            if (!Enum.TryParse(config["MinimumLogLevel"], out _minimumLogLevel))
            {
                throw new ArgumentNullException(config["MinimumLogLevel"], "Required configuration parameter is null!");
            }
            


        }
    }
}
