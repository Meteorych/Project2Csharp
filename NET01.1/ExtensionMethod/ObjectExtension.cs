using ConsoleApp1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.ExtensionMethod
{
    static class ObjectExtension
    {
        static public void GenerateNewId(this ITraining trainingObject) 
        { 
            trainingObject.Id = Guid.NewGuid();
        }  
    }
}
