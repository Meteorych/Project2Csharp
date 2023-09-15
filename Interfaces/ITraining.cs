using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Interfaces
{
    internal interface ITraining
    {
        public Guid Id { get; }
        public string? Description { get; set; }
    }
}
