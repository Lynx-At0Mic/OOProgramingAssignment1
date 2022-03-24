using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903M_Assessment_1_Base_Code
{
    /// <summary>
    /// Used to generate reports and format them
    /// </summary>
    public class Report
    {
        private Analyse _data;
        private int _padding;
        public Report(in Analyse data, int padding=2)
        {
            _data = data;
            _padding = padding;
        }
        /// <summary>
        /// Outputs a table to the console
        /// </summary>
        public void outputConsole()
        {
            Console.WriteLine("Text elements");
            Console.WriteLine(generateTable(_data.Elements));
            Console.WriteLine("\nCharacter frequency");
            Console.WriteLine(generateTable(_data.Characters));
        }

        /// <summary>
        /// Generates a table for given data
        /// </summary>
        /// <param name="data">Dictionary of data for table</param>
        /// <returns>string: Formatted table</returns>
        private string generateTable(Dictionary<string, int> data)
        { 
            int maxLen = 0;
            // Calculate max length of key present in table
            foreach (var key in data.Keys)
            {
                if (maxLen < key.Length)
                {
                    maxLen = key.Length;
                }
            }
            // Format of column
            string colFormatString = "| {0} ";
            // Calculate length of table line and repeat '-' for that length
            string bar = new string('-', (colFormatString.Replace("{0}", "").Length + maxLen + _padding) * 2) + '\n';
            string table = bar; // Top horizontal line
            
            // Loop over key value pairs in data and generate rows
            foreach (var row in data)
            {
                table += new DataTableRow(colFormatString, new[] {row.Key, row.Value.ToString()})
                    .GetRow(maxLen, _padding);
            }

            table += bar; // Bottom horizontal line

            return table;
        }
    }
}

/// <summary>
/// Utility class used to generate table rows
/// </summary>
public class DataTableRow
{
    private readonly string _formatString;
    private readonly string[] _values;
    public DataTableRow(string formatString, string[] values)
    {
        _formatString = formatString;
        _values = values;
    }

    /// <summary>
    /// Generates a formatted table row for given data
    /// </summary>
    /// <param name="maxLen">Max length of table column</param>
    /// <param name="padding">Padding to add to end of columns</param>
    /// <returns>string: Formatted table row</returns>
    public string GetRow(int maxLen, int padding)
    {
        string row = string.Empty;
        foreach (var value in _values)
        {
            row += string.Format(_formatString + new string(' ', (maxLen + padding) - value.Length), value);
        }

        return row + "|\n";
    }
}