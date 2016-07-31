using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VidzyExerciseSection3Class20
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new VidzyContexts();
            context.AddVideo("El padrino", new DateTime(1987,01,31), 1, (byte)Classification.Gold);
            
        }
    }
}
