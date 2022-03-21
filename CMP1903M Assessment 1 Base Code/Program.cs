//Base code project for CMP1903M Assessment 1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CMP1903M_Assessment_1_Base_Code
{
    class Program
    {
        static void Main()
        {
            //Local list of integers to hold the first five measurements of the text
            // List<int> parameters = new List<int>();

            //Create 'Input' object
            //Get either manually entered text, or text from a file
            Input ioReader = new Input();
            string testString = "";
            
            while (true)
            {
                Console.WriteLine("Options:\n1: Manual input\n2: File input\n3: Exit");
                string usrInput = Console.ReadLine();
                if (usrInput == "1")
                {
                    Console.WriteLine("Enter text");
                    testString = ioReader.manualTextInput();
                }
                else if (usrInput == "2")
                {
                    Console.WriteLine("Enter absolute file path");
                    testString = ioReader.fileTextInput(Console.ReadLine());
                }
                else if (usrInput == "3") break;
                else
                {
                    Console.WriteLine("Invalid input!");
                    continue;
                }
                
                //Create an 'Analyse' object
                //Pass the text input to the 'analyseText' method
                //Receive a list of integers back
                var dict = Analyse.AnalyseText(testString);

                //Report the results of the analysis
                Console.WriteLine("\n");
                foreach (var key in dict.Keys)
                {
                    Console.WriteLine("{0}:   \t{1}", key, dict[key]);
                }

                Console.WriteLine("\n");
                
                //TO ADD: Get the frequency of individual letters?
                
            }
        }
    }
}
