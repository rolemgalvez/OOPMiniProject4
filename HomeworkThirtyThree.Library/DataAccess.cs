using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace HomeworkThirtyThree.Library
{
    public class DataAccess<T> where T: new()
    {
        public event EventHandler<T> BadEntryFound;

        public void SaveToCSV(List<T> items, string filePath)
        {
            List<string> rows = new List<string>();
            T entry = new T();
            var columns = entry.GetType().GetProperties();
            string row = "";

            rows.Add(GetHeader(columns, row));
            rows = IncludeContent(items, rows, columns, row);

            File.WriteAllLines(filePath, rows);
        }

        private List<string> IncludeContent(List<T> items, List<string> rows, PropertyInfo[] columns, string row)
        {
            foreach (var item in items)
            {
                row = "";
                bool badWordDetected = false;

                foreach (var column in columns)
                {
                    string value = column.GetValue(item).ToString();
                    badWordDetected = BadWordDetected(value);

                    if (badWordDetected)
                    {
                        BadEntryFound?.Invoke(this, item);
                        break;
                    }

                    row += $",{value}";
                }

                if (badWordDetected == false)
                {
                    row = row[1..];
                    rows.Add(row);
                }
            }

            return rows;
        }

        private bool BadWordDetected(string value)
        {
            bool output = false;
            string lowerCaseValue = value.ToLower();

            if (lowerCaseValue.Contains("darn") || lowerCaseValue.Contains("heck"))
            {
                output = true;
            }

            return output;
        }

        private static string GetHeader(PropertyInfo[] columns, string row)
        {
            foreach (var column in columns)
            {
                row += $",{column.Name}";
            }
            row = row[1..];
            return row;
        }
    }
}
