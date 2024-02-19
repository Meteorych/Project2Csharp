﻿using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using NET02._4.Crawler;

namespace NET02._4.CrawlerFabric
{
    /// <summary>
    /// Abstract fabric for creating implementations of ICrawler interface.
    /// </summary>
    public interface ICrawlerFabric
    {
        public ICrawler Create(IConfigurationSection config, HttpClient httpClient, SmtpClient? smtpClient = null, MimeMessage? message = null);
    }
}
