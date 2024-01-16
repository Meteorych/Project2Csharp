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
        private readonly Document _workPart;
        public EventHandler<EventListenerArgs>? Events;

        public WordListener(ListenerOptions options)
        {
            if (string.IsNullOrEmpty(options.FilePath))
            {
                throw new ArgumentNullException(nameof(options.FilePath), "File path can't be empty");
            }
            
            using var doc = WordprocessingDocument.Create(options.FilePath, WordprocessingDocumentType.Document);
            var mainPart = doc.AddMainDocumentPart();
            mainPart.Document = new Document();
            _workPart = mainPart.Document;
            _document = doc;
        }

        public void LogMessage(string message)
        {
            lock (_lockObject)
            {
                var body = new Body(new Paragraph(new Run(new Text(message))));
                _workPart.Append(body);
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
