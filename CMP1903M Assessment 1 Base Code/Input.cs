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

        public string GetInput()
        {
            while (true)
            {
                Console.WriteLine("Options:\n1: Manual input\n2: File input\n3: Exit");
                string usrInput = Console.ReadLine() ?? string.Empty;
                string analysisText;
                switch (usrInput)
                {
                    case "1": // CLI text entry
                        analysisText = manualTextInput();
                        break;
                    case "2": // Read file
                        analysisText = fileTextInput();
                        break;
                    case "3": // Exit
                        throw new UserTerminationException("User exited");
                    default:
                        Console.WriteLine("Invalid input!");
                        continue;
                }

                return analysisText;
            }
        }
        
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
                Console.WriteLine("Enter the full filepath of the text file\n" +
                                  "Enter nothing to use test file: ");
                string fileName = Console.ReadLine() ?? string.Empty;

                if (fileName == String.Empty || fileName == null)
                {
                    fileName = "./../../../test_file.txt";
                }

                try
                {
                    // Create StreamReader to read file
                    string contents = File.ReadAllText(fileName);
                    if (contents.IndexOf('*') == -1) // check if a terminator is in the file
                    {
                        return contents;
                    }
                    return contents.Substring(0, contents.IndexOf('*'));
                }
                catch (IOException e)
                {
                    Console.WriteLine("Error, could not read the file!");
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Error, could not read the file!");
                }
            }
        }
    }
}

public class UserTerminationException : Exception
{
    public UserTerminationException() { }

    public UserTerminationException(string name)
    : base("User exited") { }
}
