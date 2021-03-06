using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903M_Assessment_1_Base_Code
{
    /// <summary>
    /// Used to generate reports and format them, example of abstraction and encapsulation
    /// </summary>
    public class Report
    {
        private Analyse _data;
        public Report(in Analyse data)
        {
            _data = data;
        }
        /// <summary>
        /// Outputs a table to the console
        /// </summary>
        public void outputConsole()
        {
            Console.WriteLine("Text elements");
            Console.WriteLine(new Table("| {0} ", _data.Elements, 2).GetTable());
            Console.WriteLine("\nCharacter frequency");
            Console.WriteLine(new Table("| {0} ", _data.Characters, 2).GetTable());
            Console.WriteLine("\nLong words (a file containing these was saved to the project directory)");
            foreach (string word in _data.LongWords)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine("\n");
            if (!WriteLongWordsFile())
            {
                Console.WriteLine("Could not write long words file! Something is wrong!");
            }
        }

        /// <summary>
        /// Creates text file containing long words found in text
        /// </summary>
        /// <returns>bool: True if successful</returns>
        public bool WriteLongWordsFile()
        {
            string longWordsFilePath = "./../../../long_words.txt";
            if (File.Exists(longWordsFilePath)) // Check if file exists
            {
                File.Delete(longWordsFilePath);
            }

            // Write words to file
            try
            {
                using StreamWriter sw = File.CreateText(longWordsFilePath);
                foreach (string word in _data.LongWords)
                {
                    sw.WriteLine(word);
                }
            }
            catch (DirectoryNotFoundException e)
            {
                return false;
            }
            catch (PathTooLongException e)
            {
                return false;
            }

            return true;

        }
    }
}

/// <summary>
/// Utility class used to generate table rows, example of abstraction and encapsulation
/// </summary>
public class Table
{
    private readonly string _formatString;
    private readonly Dictionary<string, int> _data;
    private readonly int _padding;
    
    /// <summary>
    /// Creates table object
    /// </summary>
    /// <param name="columnFormat">string: Specifies format of column, uses {0} to insert data into string
    /// For example, "| {0} " with product columns that look like "| key | value "
    /// </param>
    /// <param name="data">Dictionary of data to create table with</param>
    /// <param name="padding">Minimum padding to add to end on columns</param>
    public Table(string columnFormat, Dictionary<string, int> data, int padding)
    {
        _formatString = columnFormat;
        _data = data;
        _padding = padding;
        
    }

    /// <summary>
    /// Generates a table for given data
    /// </summary>
    /// <returns>string: Formatted table</returns>
    public string GetTable()
    {
        int maxLen = 0;
        // Calculate max length of key present in table
        foreach (var key in _data.Keys)
        {
            if (maxLen < key.Length)
            {
                maxLen = key.Length;
            }
        }
        // Calculate length of table line and repeat '-' for that length
        string bar = new string('-', (_formatString.Replace("{0}", "").Length + maxLen + _padding) * 2) + '\n';
        string table = bar; // Top horizontal line
            
        // Loop over key value pairs in data and generate rows
        foreach (var row in _data)
        {
            table += GetRow(new[] {row.Key, row.Value.ToString()}, maxLen);
        }

        table += bar; // Bottom horizontal line

        return table;
    }

    /// <summary>
    /// Generates a formatted table row for given data
    /// </summary>
    /// <param name="rowValues">Values to create table row with</param>
    /// <param name="maxLen">Max length of table column</param>
    /// <param name="padding">Padding to add to end of columns</param>
    /// <returns>string: Formatted table row</returns>
    private string GetRow(string[] rowValues, int maxLen)
    {
        string row = string.Empty;
        foreach (var value in rowValues)
        {
            row += string.Format(_formatString + new string(' ', (maxLen + _padding) - value.Length), value);
        }

        return row + "|\n";
    }
}