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
                // Get input from user
                try
                {
                    analysisText = ioReader.GetInput();
                }
                catch (UserTerminationException) // Catch exception when user exits
                {
                    return;
                }

                // Initialise analyse object with analysisText
                var analysis = new Analyse
                {
                    AnalysisText = analysisText
                };

                Console.WriteLine("\n");
                
                // Create report object and output to console
                var output = new Report(in analysis);
                output.outputConsole();

            }
        }
    }
}
