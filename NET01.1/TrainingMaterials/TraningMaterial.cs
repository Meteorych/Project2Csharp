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
        private const int maxlength = 256;
        public Guid Id { get; set; }
        private string? _description;
        public string? Description { get { return _description; } set {
                if (value?.Length > maxlength)
                {
                    throw new ArgumentException("Wrong length!");
                }
               
                 _description = value;
                } 
        }
        public override string? ToString()  { return _description; }
        //Переопределить Equals класиический (в том числе не забыть про проверку TrainingMaterial ли это
        public override bool Equals(object? otherId)
        {
            return Id.Equals(otherId);
        }
    }
}
