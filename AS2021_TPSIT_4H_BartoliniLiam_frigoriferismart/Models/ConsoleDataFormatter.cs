using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AS2021_TPSIT_4H_BartoliniLiam_frigoriferismart.Models
{
    class ConsoleDataFormatter
    {
        const int TableWidth = 80;
        public static void PrintSeperatorLine()
        {
            Console.WriteLine(new string('-', TableWidth));
        }

        static public void PrintRows(params string[] columns)
        {
            int columnWidth = (TableWidth - columns.Length) / columns.Length;
            const string seed = "|";

            string row = columns.Aggregate("|", (separator, columnsText) => separator + GetCenterAllignedText(columnsText, columnWidth) + seed);
            Console.WriteLine(row);
        }

        static string GetCenterAllignedText(string columnText, int columnWidth)
        {
            columnText = columnText.Length > columnWidth ? columnText.Substring(0, columnWidth - 3) + "..." : columnText;

            return string.IsNullOrEmpty(columnText) ? new string(' ', columnWidth) : columnText.PadRight(columnWidth - ((columnWidth - columnText.Length) / 2)).PadLeft(columnWidth);
        }
    }
}
