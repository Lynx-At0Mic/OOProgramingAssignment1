//Base code project for CMP1903M Assessment 1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CMP1903M_Assessment_1_Base_Code
{
    /// <summary>
    /// Main program class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry method
        /// </summary>
        static void Main()
        {
            // Initialize IO reader
            Input ioReader = new Input();
            string analysisText;
            
            // Loop until exit
            while (true)
            {
                Console.WriteLine("Options:\n1: Manual input\n2: File input\n3: Exit");
                string usrInput = Console.ReadLine() ?? string.Empty;
                switch (usrInput)
                {
                    case "1": // CLI text entry
                        analysisText = ioReader.manualTextInput();
                        break;
                    case "2": // Read file
                        analysisText = ioReader.fileTextInput();
                        break;
                    case "3": // Exit
                        return;
                    default:
                        Console.WriteLine("Invalid input!");
                        continue;
                }
                
                // Initialise analyse object with analysisText
                var analysis = new Analyse
                {
                    AnalisisText = analysisText
                };

                Console.WriteLine("\n");
                
                // Report
                foreach (var key in analysis.Elements.Keys)
                {
                    Console.WriteLine("{0}:   \t{1}", key, analysis.Elements[key]);
                }

                Console.WriteLine("\n");

            }
        }
    }
}
