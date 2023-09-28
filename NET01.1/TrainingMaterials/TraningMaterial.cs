using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.TrainingMaterials
{
    abstract class TrainingMaterial : BaseEntity
    {
        new public string? ToString { get { return _description; }}
        public bool Equals(string otherId)
        {
            return Id.Equals(otherId);
        }
    }
}
