using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Interfaces
{
    internal interface IVersionable
    {
        public byte[] GetVersion(byte[] version);
        public void SetVersion(byte[] version);
    }
}
