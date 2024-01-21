using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET02._4.Crawler
{
    public class CrawlerOptions
    {
        public TimeSpan Timeout { get; set; }
        public TimeSpan MaxWaitingTime { get; set; }
        public string? Url { get; set; }
        public string? MailAddress { get; set; }
    }
}
