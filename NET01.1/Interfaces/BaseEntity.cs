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
        protected string? _description = null;
        public string? Description
        {
            get { return _description; }
            set
            {
                   _description = value;
            }
        }
    }
}
