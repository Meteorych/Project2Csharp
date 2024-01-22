using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET02._4.Crawler;

/// <summary>
/// Crawler's interface.
/// </summary>
public interface ICrawler
{
    public void Start();
    public void Stop();
}