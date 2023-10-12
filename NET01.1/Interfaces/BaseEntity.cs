using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Interfaces
{
    internal abstract class BaseEntity
    {
        public Guid Id { get; set;}
        public string? Description { get; set; }
    }
}
