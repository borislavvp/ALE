using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace logic.ExpressionService.Common.Models
{
    public class TruthTableValues : Dictionary<string, List<string>>, ICloneable
    {
        public TruthTableValues() : base() { }
        public TruthTableValues(int capacity) : base(capacity) { }

        public object Clone()
        {
            TruthTableValues clone = new TruthTableValues();
            foreach (KeyValuePair<string, List<string>> kvp in this)
            {
                clone.Add(kvp.Key, kvp.Value.ToList());
            }
            return clone;
        }
    }
}
