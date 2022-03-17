using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CMP1903M_Assessment_1_Base_Code
{
    public static class Analyse
    {
        //Handles the analysis of text

        //Method: analyseText
        //Arguments: string
        //Returns: list of integers
        //Calculates and returns an analysis of the text
        public static Dictionary<string, int> AnalyseText(string input)
        {
            //List of integers to hold the first five measurements:
            //1. Number of sentences
            //2. Number of vowels
            //3. Number of consonants
            //4. Number of upper case letters
            //5. Number of lower case letters
            //Initialise all the values in the list to '0'
            var tools = new Dictionary<string, Func<string, int>>()
            {
                {"sentences", s => Regex.Match(s, "/(\\s|^)+[^.!?\\n]*[.!?\\n]/gm").Length},
                {"vowels", s => Regex.Match(s, "/[aeiou]/gm").Length},
                {"consonants", s => Regex.Match(s, "/[^aeiou]/gm").Length},
                {"upper", s => Regex.Match(s, "/[A-Z]/gm").Length},
                {"lower", s => Regex.Match(s, "/[a-z]/gm").Length}
            };

            return tools.Keys.ToDictionary(toolsKey => toolsKey, toolsKey => new AnalysisTool(tools[toolsKey]).Count(input));
        }
    }
    
    public class AnalysisTool
    {
        private readonly Func<string, int> _tool;
        public AnalysisTool(Func<string, int> tool)
        {
            this._tool = tool;
        }

        public int Count(string text)
        {
            return this._tool(text);
        }
    }
}
