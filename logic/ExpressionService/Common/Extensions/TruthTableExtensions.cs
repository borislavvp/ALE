using logic.ExpressionService.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logic.ExpressionService.Common.Extensions
{
    public static class TruthTableExtensions
    {
        public static List<string> GetRowValuesWithouthResultColumn(this TruthTableValues values, int row)
        {
            var list = new List<string>();
            foreach (var key in values.Keys)
            {
                if (key != values.Keys.Last())
                {
                    list.Add(values[key][row]);
                }
            }
            return list;
        }
        public static void DeleteRowValues(this TruthTableValues values, int row)
        {
            foreach (var key in values.Keys)
            {
                values[key].RemoveAt(row);
            }
        }
    }
}
