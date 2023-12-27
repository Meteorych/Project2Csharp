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
    public class WordListener : IListener
    {
        private readonly string _filePath;
        public EventHandler<EventListenerArgs>? Events;

        public WordListener(string path)
        {
            _filePath = path;
        }

        public void LogMessage(string message)
        {
            using var doc = WordprocessingDocument.Create(_filePath, WordprocessingDocumentType.Document);
            var mainPart = doc.AddMainDocumentPart();
            mainPart.Document = new Document();
            var body = new Body(new Paragraph(new Run(new Text(message))));
            mainPart.Document.Append(body);
        }
    }
}
