using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Listener;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace WordListeners
{
    /// <summary>
    /// WordListener class for logging into *.docx files.
    /// </summary>
    public class WordListener : AbstractListener, IDisposable
    {
        private readonly object _lockObject = new();
        private readonly WordprocessingDocument _document;
        private readonly Body _workPart;
        public EventHandler<EventListenerArgs>? Events;

        public WordListener(ListenerOptions options)
        {
            if (string.IsNullOrEmpty(options.FilePath))
            {
                throw new ArgumentNullException(nameof(options.FilePath), "File path can't be empty");
            }
            Options = options;
            MainDocumentPart mainPart;
            if (File.Exists(Options.FilePath))
            {
                _document = WordprocessingDocument.Open(Options.FilePath, true);
                mainPart = _document.MainDocumentPart ?? _document.AddMainDocumentPart();
                mainPart.Document ??= new Document();
            }
            else
            {
                _document = WordprocessingDocument.Create(Options.FilePath, WordprocessingDocumentType.Document);
                mainPart = _document.AddMainDocumentPart();
                mainPart.Document = new Document();
            }

            if (mainPart.Document.Body is null)
            {
                mainPart.Document.Append(new Body(new Run(new Title("Logs:"))));
                _document.Save();
            }

            mainPart.Document.Body ??= new Body();
            _workPart = mainPart.Document.Body;
            
        }

        public override void LogMessage(string message)
        {
            if (!IsLogLevelEnabled(message)) return;
            lock (_lockObject)
            {
                var paragraph = new Paragraph(new Run(new Text($"{DateTime.Now}: {message}")));
                _workPart.Append(paragraph);
                _document.Save();
            }
        }

        public void Dispose()
        {
            _document.Dispose();
            GC.SuppressFinalize(this);
        }

        ~WordListener()
        {
            _document.Save();
            Dispose();
        }
    }
}
