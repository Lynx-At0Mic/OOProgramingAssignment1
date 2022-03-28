using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903M_Assessment_1_Base_Code
{
    /// <summary>
    /// Used to accept both manual text input and text from a file
    /// </summary>
    public class Input
    {
        /// <summary>
        /// Used to accept text input through the command line
        /// </summary>
        /// <returns>string: Text input</returns>
        public string manualTextInput()
        {
            Console.WriteLine("Enter text, use '*' to end text entry");
            string input = String.Empty;
            while (true)
            {
                try
                {
                    while (!input.EndsWith('*')) // Loop until user ends entry with '*'
                    {
                        input += Console.ReadLine() ?? string.Empty;
                    }

                    return input.Trim('*'); // Remove '*' and return input
                }
                catch (IOException e)
                {
                    Console.WriteLine("An IO error occured, assuming user entered nothing");
                    return string.Empty;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Line is too long! Try again");
                }
            }
        }
        
        /// <summary>
        /// Used to get filepath from user and read contents
        /// </summary>
        /// <returns>string: File contents</returns>
        public string fileTextInput()
        {
            while (true)
            {
                Console.WriteLine("Enter the filepath of the text file:");
                string fileName = Console.ReadLine() ?? string.Empty;

                try
                {
                    // Create StreamReader to read file
                    return File.ReadAllText(fileName).Trim('*');
                }
                catch (IOException e)
                {
                    Console.WriteLine("Error, could not read the file!");
                }
            }
        }
    }
}
