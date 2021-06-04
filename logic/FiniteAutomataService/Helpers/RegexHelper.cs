using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace logic.FiniteAutomataService.Helpers
{
    public static class RegexHelper
    {
        public static string Match(string input,string pattern)
        {
            var rg = Regex.Match(input.Replace("\r",""), pattern);
            if (!rg.Success)
            {
                return String.Empty;
            }
            else
            {
                return rg.Captures[0].Value.Trim();
            }
        }
    }
}
