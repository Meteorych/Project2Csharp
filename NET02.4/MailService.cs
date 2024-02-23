using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using NLog.Targets;

namespace NET02._4
{
    public class MailService : IDisposable
    {
        private readonly SmtpClient _smtpClient = new();
        private readonly MimeMessage _message = new();

        /// <summary>
        /// Initializing Smtp Client and sender for message
        /// </summary>
        /// <param name="senderMailAddress">Mail address of sender.</param>
        /// <param name="password">Password for sender mail address.</param>
        /// <param name="senderName">Name of sender app.</param>
        public void InitializeMessage(string senderMailAddress, string password, string senderName)
        {
            _smtpClient.Connect("smtp.mail.ru", 465, true);
            _smtpClient.Authenticate(senderMailAddress, password);
            _message.From.Add(new MailboxAddress(senderName, senderMailAddress));

        }

        /// <summary>
        /// Initializing receiver and message body.
        /// </summary>
        /// <param name="receiverAddress">Address of the receiver.</param>
        /// <param name="messageSubject">Subject of message.</param>
        /// <param name="messageText">Text of message.</param>
        public void InitializeMessageBody(MailboxAddress receiverAddress, string messageSubject, TextPart messageText)
        {
            _message.To.Add(receiverAddress);
            _message.Subject = messageSubject;
            _message.Body = messageText;
        }

        /// <summary>
        /// Sending message.
        /// </summary>
        /// <returns></returns>
        public async Task SendMessage()
        {
            await _smtpClient.SendAsync(_message);
        }

        public void Dispose()
        {
            _smtpClient.Dispose();
            _message.Dispose();
        }
    }
}
