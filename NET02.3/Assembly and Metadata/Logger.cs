using System.Reflection;
using Assembly_and_Metadata.Attributes;
using Listeners;
using TextListeners;
using WordListeners;
using EventLogListeners;
using Microsoft.Extensions.Configuration;

namespace Assembly_and_Metadata
{
    public class MyLogger
    {
        private readonly IConfiguration _configuration;
        private readonly List<IListener> _listeners = new();

        public MyLogger(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Method for initializing listeners using settings from configuration.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public void InitializeListeners()
        {
            var listenerConfigurations = _configuration.GetSection("Listeners").Get<List<ListenerOptions>>();
            if (listenerConfigurations is null)
            {
                throw new ArgumentNullException(nameof(listenerConfigurations));
            }
            foreach (var listenerConfiguration in listenerConfigurations)
            {
                var listener = CreateListener(listenerConfiguration);
                _listeners.Add(listener);
            }
        }

        public void LogMessage(string message)
        {
            foreach (var listener in _listeners)
            {
                listener.LogMessage(message);
            }
        }
        /// <summary>
        /// Method for tracking classes and its methods.
        /// </summary>
        /// <param name="trackObject">Object for tracking.</param>
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
        /// <summary>
        /// Method for creating listener.
        /// </summary>
        /// <param name="options">Listener options.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TypeLoadException"></exception>
        private IListener CreateListener(ListenerOptions options)
        {
            var listenerTypeName = options.ListenerType;
            if (string.IsNullOrEmpty(listenerTypeName))
            {
                throw new ArgumentNullException(nameof(listenerTypeName), "Listener type option is null or empty.");
            }

            var listenerType = Type.GetType(listenerTypeName, true, true) ?? throw new TypeLoadException("Unable to load type.");
            var listener = Activator.CreateInstance(listenerType, options);
            if (listener == null)
            {
                throw new ArgumentNullException(nameof(listener), "Listener doesn't exist.");
            }
            return (IListener)listener;
        }
    }
}
