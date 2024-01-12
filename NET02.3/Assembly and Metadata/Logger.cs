using System.Reflection;
using Assembly_and_Metadata.Attributes;
using EventLogListeners;
using Listeners;
using Microsoft.Extensions.Configuration;
using TextListeners;
using WordListeners;

namespace Assembly_and_Metadata
{
    public class MyLogger
    {
        private readonly IConfiguration _configuration;
        private readonly Dictionary<string, IListener> _listeners = new();

        public MyLogger(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void InitializeListeners()
        {
            _listeners.Add("TextListeners", new TextListener(_configuration.GetSection("NLog:targets:textFile:fileName").Value ?? "logwrong.txt"));
            _listeners.Add("WordListener", new WordListener(_configuration.GetSection("NLog:targets:wordFile:fileName").Value ?? "logwrong.docx"));
            _listeners.Add("EventLogListener", new EventLogListener());
        }

        public void LogMessage(string message)
        {
            foreach (var pair in _listeners)
            {
                pair.Value.LogMessage(message);
            }
        }

        public void Track(object trackObject)
        {
            if (trackObject.GetType().GetCustomAttribute(typeof(TrackingEntity), true) is null) return;

            var properties = trackObject
                .GetType()
                .GetProperties()
                .Where(property => property.GetCustomAttribute<TrackingProperty>() is not null);
            var fields = trackObject
                .GetType()
                .GetFields()
                .Where(field => field.GetCustomAttribute<TrackingProperty>() is not null);

            foreach (var property in properties)
            {
                LogMessage($"{property.Name} - {property.GetValue(trackObject)}");
            }

            foreach (var field in fields)
            {
                LogMessage($"{field.Name} - {field.GetValue(trackObject)}");
            }
        }
    }
}
