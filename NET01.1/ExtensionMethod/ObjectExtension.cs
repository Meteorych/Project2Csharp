using ConsoleApp1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.ExtensionMethod
{
    class ObjectExtension
    {
        static public void GenerateNewId(ITraining trainingObject) 
        { 
            trainingObject.Id = Guid.NewGuid();
        }  
    }
}
