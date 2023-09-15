using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Interfaces;

namespace ConsoleApp1.TrainingMaterials
{
    abstract class TrainingMaterial : ITraining
    {
        public Guid Id { get; protected set; }
        private string? _description;
        public string? Description { get { return _description; } set {
                if (value.Length > 256)
                {
                    _description = value[..256];
                }
                else
                {
                    _description = value;
                }
                } 
        }
        new public string? ToString { get { return _description; }}
        public bool Equals(string otherId)
        {
            return Id.Equals(otherId);
        }
    }
}
