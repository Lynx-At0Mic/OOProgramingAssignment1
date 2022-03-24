using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CMP1903M_Assessment_1_Base_Code
{
    /// <summary>
    /// Analyses text
    /// </summary>
    public class Analyse
    {
        // Text to analyse, runs AnalyseText method when set
        private string _analysisText;
        public string AnalisisText
        {
            get { return _analysisText; }
            set
            {
                _analysisText = value;
                AnalyseText(_analysisText);
            }
        }

        public Dictionary<string, int> Elements { get; private set; }
        public Dictionary<string, int> Characters { get; private set; }

        public Analyse()
        {
            
        }
        /// <summary>
        /// Runs analysis methods
        /// </summary>
        /// <param name="input">Text to analyse</param>
        protected void AnalyseText(string input)
        {
            Elements = CountElements(input);
            Characters = CountChars(input);
        }

        /// <summary>
        /// Analyses text using instances of AnalysisTool
        /// </summary>
        /// <param name="text">Text to analyse</param>
        /// <returns>dict: Keys as text elements and values corresponding to count</returns>
        protected Dictionary<string, int> CountElements(string text)
        {
            //List of integers to hold the first five measurements:
            //1. Number of sentences
            //2. Number of vowels
            //3. Number of consonants
            //4. Number of upper case letters
            //5. Number of lower case letters
            
            // Create dictionary with anonymous functions that count elements using Regex
            var tools = new Dictionary<string, Func<string, int>>()
            {
                {"sentences", s => Regex.Matches(s, @"(\s|^)+[^.!?]*([.!?]|\S)", RegexOptions.Multiline).Count()},
                {"vowels", s => Regex.Matches(s, @"[aeiouAEIOU]", RegexOptions.Multiline).Count()},
                {"consonants", s => Regex.Matches(s, @"[^aeiouAEIOU\W\s\d]", RegexOptions.Multiline).Count()},
                {"upper", s => Regex.Matches(s, @"[A-Z]", RegexOptions.Multiline).Count()},
                {"lower", s => Regex.Matches(s, @"[a-z]", RegexOptions.Multiline).Count()}
            };
            
            // Return a dictionary. Use Linq to create a dictionary with elements as keys and returned values from
            // instances of AnalysisTool as values
            return tools.Keys.ToDictionary(toolsKey => toolsKey, toolsKey => new AnalysisTool(tools[toolsKey]).Count(text));
        }

        protected Dictionary<string, int> CountChars(string text)
        {
            var dict = new Dictionary<string, int>();
            foreach (Match match in Regex.Matches(text.ToLower(), @"[\w\S]"))
            {
                if (!dict.ContainsKey(match.ToString()))
                {
                    dict.Add(match.ToString(), 1);
                }
                else
                {
                    dict[match.ToString()] += 1;
                }
            }

            return dict;
        }
    }
    
    /// <summary>
    /// Utility class used to analyse text
    /// </summary>
    public class AnalysisTool
    {
        // Readonly Func used for analysing text
        private readonly Func<string, int> _tool;
        
        /// <summary>
        /// Sets up tool
        /// </summary>
        /// <param name="tool">Function used to analyse text</param>
        public AnalysisTool(Func<string, int> tool)
        {
            _tool = tool;
        }
        /// <summary>
        /// Uses _tool to analyse text
        /// </summary>
        /// <param name="text">Text to analyse</param>
        /// <returns>int: Number of occurrences in text</returns>
        public int Count(string text)
        {
            return _tool(text);
        }
    }
}
