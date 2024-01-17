using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Listeners;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace WordListeners
{
    /// <summary>
    /// WordListener class for logging into *.docx files.
    /// </summary>
    public class WordListener : IListener, IDisposable
    {
        private readonly object _lockObject = new();
        private readonly WordprocessingDocument _document;
        private readonly ListenerOptions _options;
        private readonly Document _workPart;
        public EventHandler<EventListenerArgs>? Events;

        public WordListener(ListenerOptions options)
        {
            if (string.IsNullOrEmpty(options.FilePath))
            {
                throw new ArgumentNullException(nameof(options.FilePath), "File path can't be empty");
            }
            _options = options;
            using var doc = WordprocessingDocument.Create(_options.FilePath, WordprocessingDocumentType.Document);
            var mainPart = doc.AddMainDocumentPart();
            mainPart.Document = new Document();
            _workPart = mainPart.Document;
            _document = doc;
        }

        public void LogMessage(string message)
        {
            if (!IsLogLevelEnabled(message)) return;
            lock (_lockObject)
            {
                var body = new Body(new Paragraph(new Run(new Text(message))));
                _workPart.Append(body);
            }
        }

        private bool IsLogLevelEnabled(string message)
        {
            var comparisonOption = StringComparison.InvariantCultureIgnoreCase;
            switch (_options.MinimumLogLevel)
            {
                case LogLevels.Trace:
                    return true; //All levels
                case LogLevels.Debug:
                    return (message.Contains("DEBUG", comparisonOption) || message.Contains("INFO", comparisonOption) ||
                            message.Contains("WARN", comparisonOption)
                            || message.Contains("ERROR", comparisonOption));
                case LogLevels.Info:
                    return (message.Contains("INFO", comparisonOption) ||
                            message.Contains("WARN", comparisonOption)
                            || message.Contains("ERROR", comparisonOption));
                case LogLevels.Warn:
                    return (message.Contains("WARN", comparisonOption)
                            || message.Contains("ERROR", comparisonOption));
                case LogLevels.Error:
                    return message.Contains("ERROR", comparisonOption);
                default:
                    return false;
            }
        }

        public void Dispose()
        {
            _document.Dispose();
            GC.SuppressFinalize(this);
        }

        ~WordListener()
        {
            Dispose();
        }
    }
}
